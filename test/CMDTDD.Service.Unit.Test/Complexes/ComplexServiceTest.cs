using CMDTDD.Entities;
using CMDTDD.Persistanse.EF;
using CMDTDD.Services.Complexes.Contracts.Dto;
using CMDTDD.Services.Complexes.Exceptions;
using CMS.Service.Unit.Test.DataBaseConfig;
using CMS.Service.Unit.Test.DataBaseConfig.Unit;
using CMS.Service.Unit.Test.Factory;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace CMS.Service.Unit.Test.Complexes;

public class ComplexServiceTest : BusinessUnitTest
{
    private readonly ComplexService _sut;


    public ComplexServiceTest()
    {
        var repasitory = new EFComplexRepasitory(SetupContext);
        var unitOfWork = new EfUnitOfWork(SetupContext);
        _sut = new ComplexAppService(repasitory, unitOfWork);
    }

    [Fact]
    public void Add_Certain_add_a_complex()
    {
        var dto = new AddComplexDto()
        {
            Name = "Dummy",
            UnitCount = 10
        };

        _sut.Add(dto);

        var expectet = ReadContext.Set<Complex>();
        expectet.Single().Name.Should().Be(dto.Name);
        expectet.Single().UnitCount.Should().Be(dto.UnitCount);
    }

    [Fact]
    public void
        GetAllComplexesWithUnitCountDeatail_Certain_get_all_with_unit_count()
    {
        var complex = ComplexFactory.Create();
        var block = BlockFactory.Create(complex);
        var unit = UnitFatory.Create(block);
        DbContext.Save(unit);
        var leftUnitCOunt =
            complex.UnitCount - complex.Blocks.Sum(_ => _.Units.Count());
        var registsterdUnitCount = complex.Blocks.Sum(_ => _.Units.Count());

        var result =
            _sut.GetAllComplexesWithUnitCountDeatail();

        var actual = result.Single();
        actual.Name.Should().Be(complex.Name);
        actual.Id.Should().Be(complex.Id);
        actual.RegisterdUnitCount.Should().Be(registsterdUnitCount);
        actual.LeftUnitCount.Should().Be(leftUnitCOunt);
    }

    [Fact]
    public void GetAllComplexWithBlockDetails_Certain_get_with_blocks()
    {
        var complex = ComplexFactory.Create();
        var block = BlockFactory.Create(complex);
        var unit = UnitFatory.Create(block);
        DbContext.Save(unit);
        var remainingUnitCount = block.UnitCount - block.Units.Count();
        var result =
            _sut.GetAllComplexWithBlockDetails();

        var actual =
            result.Where(_ => _.Name == complex.Name).Single();
        actual.Block.Count().Should().Be(1);
        actual.Block.Any(_ => _.BlockName == block.Name).Should().BeTrue();
        actual.Block.Any(_ => _.BlockUnitCount == block.UnitCount).Should()
            .BeTrue();
        actual.Block.Any(_ => _.RemainingUnitCount == remainingUnitCount)
            .Should().BeTrue();
    }

    [Fact]
    public void GetAllComplexWithBlockDetails_Certain_blockName_not_null()
    {
        var complex = ComplexFactory.Create();
        var block = BlockFactory.Create(complex);
        var blockName = "asd";
        var block2 = BlockFactory.Create(complex,null,blockName);
        DbContext.Save(block);
        DbContext.Save(block2);

        var result =
            _sut.GetAllComplexWithBlockDetails(blockName);

        var actual = result.Single();
        actual.Name.Should().Be(complex.Name);
        actual.Block.Single().BlockName.Should().Be(blockName);
    }

    [Fact]
    public void GetAllUsageTypesById_Certain_get_complex()
    {
        var complex = ComplexFactory.Create();
        DbContext.Save(complex);
        var usageType = new UsageType()
        {
            Name = "dummy",
        };
        DbContext.Save(usageType);
        var usageType2 = new UsageType()
        {
            Name = "arosak",
        };
        DbContext.Save(usageType2);

        var complexUsageType = new ComplexUsageType()
        {
            ComplexId = complex.Id,
            UsageTypeId = usageType.Id
        };
        DbContext.Save(complexUsageType);

        var complexUsageType2 = new ComplexUsageType()
        {
            ComplexId = complex.Id,
            UsageTypeId = usageType2.Id
        };
        DbContext.Save(complexUsageType2);

        var reseult =
            _sut.GetAllUsageTypesById(complex.Id);

        reseult.Should().HaveCount(2);
        var a = new List<GetAllUsageTypesOfComplexDto>
        {
            new()
            {
                UsageTypeId = usageType.Id,
                UsageTypeName = usageType.Name
            },
            new()
            {
                UsageTypeId = usageType2.Id,
                UsageTypeName = usageType2.Name
            }
        };
        reseult.Should().BeEquivalentTo(a);
    }

    [Fact]
    public void GetAllUsageTypesById_Certain_exception_complex_not_found()
    {
        var invalidComplexId = 0;


        var expected = () => _sut.GetAllUsageTypesById(invalidComplexId);

        expected.Should().ThrowExactly<InvalidComplexIdExeption>();
    }

    [Fact]
    public void Update_Certain_update_a_complex()
    {
        var complex = ComplexFactory.Create();
        DbContext.Save(complex);
        var dto = CreateUpdateComplexDto();

        _sut.Update(complex.Id, dto);

        var expected = ReadContext.Set<Complex>().Single(_ => _.Id == complex.Id);
        expected.UnitCount.Should().Be(dto.UnitCount);
    }

    [Fact]
    public void Update_Certain_exception_complex_has_unit()
    {
        var complex = ComplexFactory.Create();
        var block = BlockFactory.Create(complex);
        var unit = UnitFatory.Create(block);
        DbContext.Save(unit);
        var dto = CreateUpdateComplexDto();

        var expected = () => _sut.Update(complex.Id, dto);

    expected.Should().ThrowExactly<ComplexHasUnitException>();
    }


    [Fact]
    public void Update_Certain_exception_complex_not_found()
    {
        var invalidComplexId = 0;
        var dto = CreateUpdateComplexDto();

        var expected = () => _sut.Update(invalidComplexId, dto);

        expected.Should().ThrowExactly<InvalidComplexIdExeption>();
    }

    [Fact]
    public void Delete_Certain_delete_a_complex()
    {
        var complex = ComplexFactory.Create();
        DbContext.Save(complex);

        _sut.Delete(complex.Id);

        var expected = ReadContext.Set<Complex>();

        expected.Should().BeEmpty();
    }

    [Fact]
    public void Delete_Certain_exception_complex_has_units()
    {
        var complex = ComplexFactory.Create();
        var block = BlockFactory.Create(complex);
        var unit = UnitFatory.Create(block);
        DbContext.Save(unit);

        var expexted = () => _sut.Delete(complex.Id);

        expexted.Should().ThrowExactly<ComplexHasUnitException>();
    }

    [Fact]
    public void Delete_Certain_exception_invalid_complex_id()
    {
        var invalidCoplexId = 0;
        
        var expected = () => _sut.Delete(invalidCoplexId);
        
        expected.Should().ThrowExactly<InvalidComplexIdExeption>();

    }

    /////////
    private UpdateComplexDto CreateUpdateComplexDto()
    {
        return
            new UpdateComplexDto()
            {
                UnitCount = 70
            };
    }
}