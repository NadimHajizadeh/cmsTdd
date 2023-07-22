using CMDTDD.Entities;
using CMDTDD.Persistanse.EF;
using CMDTDD.Services.Complexes.Exceptions;
using CMS.Service.Unit.Test.Complexes;
using CMS.Service.Unit.Test.DataBaseConfig;
using CMS.Service.Unit.Test.DataBaseConfig.Unit;
using CMS.Service.Unit.Test.Factory;
using FluentAssertions;
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
        var dto = CreateBlockDto(complex.Id,9999);

        var expected = () => _sut.Add(dto);

        expected.Should().ThrowExactly<InvalidUnitCountException>();
    }
    
    //todo asdasdasd 
    private static AddBlockDto CreateBlockDto(int complexsId ,int? unitCount=null)
    {
        return new AddBlockDto()
        {
            Name = "Dummy",
            ComplexId = complexsId,
            UnitCount = unitCount??10
        };
    }
    
}