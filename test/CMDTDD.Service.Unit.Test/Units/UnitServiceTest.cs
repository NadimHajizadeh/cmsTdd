using System.ComponentModel.DataAnnotations;
using CMDTDD.Entities;
using CMDTDD.Persistanse.EF;
using CMDTDD.Services.Contracts;
using CMS.Service.Unit.Test.Complexes;
using CMS.Service.Unit.Test.DataBaseConfig;
using CMS.Service.Unit.Test.DataBaseConfig.Unit;
using CMS.Service.Unit.Test.Factory;
using CMS.Service.Unit.Test.Units;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace CMS.Service.Unit.Test.Units
{
    public class UnitServiceTest : BusinessUnitTest
    {
        private readonly UnitService _sut;

        public UnitServiceTest()

        {
            var repasitory = new EFUnitRepasitory(SetupContext);
            var unitOfWork = new EfUnitOfWork(SetupContext);
            _sut = new UnitAppService(unitOfWork, repasitory);
        }

        [Theory]
        [InlineData(ResidanseType.Empty)]
        [InlineData(ResidanseType.Owner)]
        [InlineData(ResidanseType.Tenant)]
        public void Add_Certain_add_a_unit(ResidanseType type)
        {
            var complex = ComplexFactory.Create();
            var block = BlockFactory.Create(complex);
            DbContext.Save(block);
            var dto = new AddUnitDto()
            {
                Name = "dummy",
                BlockId = block.Id,
                ResidenseType = type
            };

            _sut.Add(dto);

            var expected = ReadContext.Set<CMDTDD.Entities.Unit>().Single();
            expected.ResidanseType.Should().Be(type);
            expected.BlockId.Should().Be(dto.BlockId);
            expected.Name.Should().Be(dto.Name);

        }
    }

    public class AddUnitDto

    {
        [Required] [MaxLength(30)] public string Name { get; set; }

        [Required] public ResidanseType ResidenseType { get; set; }

        public int BlockId { get; set; }
    }
}


internal interface UnitService
{
    void Add(AddUnitDto dto);
}

public class UnitAppService : UnitService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly UnitRepasitory _repasitory;

    public UnitAppService(UnitOfWork unitOfWork, UnitRepasitory repasitory)
    {
        _unitOfWork = unitOfWork;
        _repasitory = repasitory;
    }

    public void Add(AddUnitDto dto)
    {
        var unit = new Unit()
        {
            Name = dto.Name,
            ResidanseType = dto.ResidenseType,
            BlockId = dto.BlockId,
        };
        _repasitory.Add(unit);
        _unitOfWork.Complete();
    }
}

public interface UnitRepasitory
{
    void Add(Unit unit);
}

public class EFUnitRepasitory : UnitRepasitory
{
    private readonly DbSet<Unit> _units;


    public EFUnitRepasitory(EFDataContext context)
    {
        _units = context.Set<Unit>();
    }

    public void Add(Unit unit)
    {
        _units.Add(unit);
    }
}