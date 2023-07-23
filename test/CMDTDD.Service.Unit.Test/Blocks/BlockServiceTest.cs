using CMDTDD.Entities;
using CMDTDD.Persistanse.EF;
using CMDTDD.Services.Complexes.Exceptions;
using CMS.Service.Unit.Test.Complexes;
using CMS.Service.Unit.Test.DataBaseConfig;
using CMS.Service.Unit.Test.DataBaseConfig.Unit;
using CMS.Service.Unit.Test.Factory;
using CMS.Service.Unit.Test.Units;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace CMS.Service.Unit.Test.Blocks;

public class BlockServiceTest : BusinessUnitTest
{
    private readonly BlockService _sut;

    public BlockServiceTest()
    {
        var unitOfWork = new EfUnitOfWork(SetupContext);
        var repasitory = new EFBlockRepasitory(SetupContext);
        var complexRepasitory = new EFComplexRepasitory(SetupContext);
        _sut = new BlockAppService(repasitory, unitOfWork, complexRepasitory);
    }

    [Fact]
    public void Add_Certain_add_a_block()
    {
        var complexs = ComplexFactory.Create();
        DbContext.Save(complexs);
        var dto = CreateBlockDto(complexs.Id);

        _sut.Add(dto);


        var expected = ReadContext.Set<Block>().Single();
        expected.Name.Should().Be(dto.Name);
        expected.UnitCount.Should().Be(dto.UnitCount);
        expected.ComplexId.Should().Be(complexs.Id);
    }


    [Fact]
    public void Add_Certain_exception_throw_invalid_complexId()
    {
        var invalidComplexId = 0;

        var dto = CreateBlockDto(invalidComplexId);

        var expected = () => _sut.Add(dto);

        expected.Should().Throw<InvalidComplexIdExeption>();
    }

    [Fact]
    public void Add_Certain_exception_invalid_unit_count()
    {
        var complex = ComplexFactory.Create();
        DbContext.Save(complex);
        var dto = CreateBlockDto(complex.Id, 9999);

        var expected = () => _sut.Add(dto);

        expected.Should().ThrowExactly<InvalidUnitCountException>();
    }


    [Fact]
    public void Add_Certain_exeption_coplex_is_full()
    {
        var complex = ComplexFactory.Create();
        var block = BlockFactory.Create(complex);
        DbContext.Save(block);
        var dto = CreateBlockDto(complex.Id, 51);

        var expoected = () => _sut.Add(dto);

        expoected.Should().ThrowExactly<ComplexIsFullException>();
    }

    [Theory]
    [InlineData(ResidenseType.Empty)]
    [InlineData(ResidenseType.Owner)]
    [InlineData(ResidenseType.Tenant)]
    public void GetAllWithUnitCountDetails_Certain_gett_all(ResidenseType type)
    {
        var complex = ComplexFactory.Create();
        var block = BlockFactory.Create(complex);
        var unit = UnitFatory.Create(block, type);
        DbContext.Save(unit);
        var existingUnitCount = 1;

        var expected = _sut.GetAllWithUnitCountDetails();

        var actual = expected.Single();
        actual.Name.Should().Be(block.Name);
        actual.UnitCount.Should().Be(block.UnitCount);
        actual.RegisterUnitCount.Should().Be(existingUnitCount);
        actual.ReminaingUnitCount.Should().Be(block.UnitCount - existingUnitCount);
    }

    [Theory]
    [InlineData(ResidenseType.Empty)]
    [InlineData(ResidenseType.Owner)]
    [InlineData(ResidenseType.Tenant)]
    public void GetOneBlock_Certain(ResidenseType type)
    {
        var complex = ComplexFactory.Create();
        var block = BlockFactory.Create(complex);
        var unit = UnitFatory.Create(block, type);
        DbContext.Save(unit);

        var expected = _sut.GetById(block.Id);

        expected.Name.Should().Be(block.Name);
        expected.UnitsInBlock.Single().Name.Should().Be(unit.Name);
        expected.UnitsInBlock.Single().Resedent.Should().Be(unit.ResidenseType.ToString());
    }

    [Fact]
    public void UpdateById_Certain_update()
    {
        var complex = ComplexFactory.Create();
        var block = BlockFactory.Create(complex);
        DbContext.Save(block);
        var dto = UpdateBlockDtoCreate();

        _sut.Update(block.Id, dto);

        var expecte = ReadContext.Set<Block>().Single();
        expecte.UnitCount.Should().Be(dto.UnitCount);
        expecte.Name.Should().Be(dto.Name);
    }


    [Fact]
    public void UpdateById_Certain_block_not_found_exception()
    {
        var invalidBlockId = 0;
        var dto = UpdateBlockDtoCreate();

        var expected = () => _sut.Update(invalidBlockId, dto);

        expected.Should().ThrowExactly<InvalidBlockIdExeption>();
    }

    [Theory]
    [InlineData(ResidenseType.Empty)]
    [InlineData(ResidenseType.Owner)]
    [InlineData(ResidenseType.Tenant)]
    public void UpdateById_Certain_block_has_units_exception(ResidenseType type)
    {
        var complex = ComplexFactory.Create();
        var block = BlockFactory.Create(complex);
        var unit = UnitFatory.Create(block, type);
        DbContext.Save(unit);

        var dto = UpdateBlockDtoCreate();

        var expected = () => _sut.Update(block.Id, dto);

        expected.Should().ThrowExactly<blockHasUnitsExeption>();
    }

    [Fact]
    public void UpdateById_Certain_blockDuplacated_Name_exception()
    {
        var complex = ComplexFactory.Create();
        var block = BlockFactory.Create(complex);
        DbContext.Save(block);

        var dto = new UpdateBlockDto()
        {
            Name = "dummy",
            UnitCount = 20
        };

        var expected = () => _sut.Update(block.Id, dto);
        expected.Should().ThrowExactly<BlockDuplicatedNameException>();
    }

    [Fact]
    public void UpdateById_Certain_ComplexIsFuLLException()
    {
        var overFlowUnitCount = 51;
        var complex = ComplexFactory.Create();
        var block = BlockFactory.Create(complex);
        DbContext.Save(block);
        var dto = UpdateBlockDtoCreate(overFlowUnitCount);

        var expected = () => _sut.Update(block.Id, dto);

        expected.Should().ThrowExactly<ComplexIsFullException>();
    }

    [Theory]
    [InlineData(ResidenseType.Empty)]
    [InlineData(ResidenseType.Owner)]
    [InlineData(ResidenseType.Tenant)]
    public void AddWithUnits(ResidenseType type)
    {
        var unitsToAdd = new List<UnitToAddBlockDto>();
        var complex = ComplexFactory.Create();
        DbContext.Save(complex);
        var blockDto = new AddBlockDto()
        {
            Name = "majid",
            ComplexId = complex.Id,
            UnitCount = 12
        };
        var dto = new UnitToAddBlockDto()
        {
            Name = "dummy",
            ResidenseType = type,
        };
        var dto2 = new UnitToAddBlockDto()
        {
            Name = "dummy2",
            ResidenseType = type,
        };
        unitsToAdd.Add(dto);
        unitsToAdd.Add(dto2);

        _sut.AddWithUnit(blockDto, unitsToAdd);

        var expected = ReadContext.Set<Block>().Include(_=>_.Units).Single();
        expected.Name.Should().Be(blockDto.Name);
        expected.Units.Count.Should().Be(2);
        expected.Units.First().ResidenseType.Should().Be(dto.ResidenseType);
        expected.Units.Last().ResidenseType.Should().Be(dto2.ResidenseType);
    }


    private static AddBlockDto CreateBlockDto(int complexsId, int? unitCount = null)
    {
        return new AddBlockDto()
        {
            Name = "Dummy",
            ComplexId = complexsId,
            UnitCount = unitCount ?? 10
        };
    }

    private static UpdateBlockDto UpdateBlockDtoCreate(int? unitCount = null)
    {
        return new UpdateBlockDto
        {
            Name = "dummy_update",
            UnitCount = unitCount ?? 10
        };
    }
}