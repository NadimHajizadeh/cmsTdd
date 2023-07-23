using CMDTDD.Entities;
using CMDTDD.Persistanse.EF;
using CMS.Service.Unit.Test.Blocks;
using CMS.Service.Unit.Test.Complexes;
using CMS.Service.Unit.Test.DataBaseConfig;
using CMS.Service.Unit.Test.DataBaseConfig.Unit;
using CMS.Service.Unit.Test.Factory;
using FluentAssertions;

namespace CMS.Service.Unit.Test.Units
{
    public class UnitServiceTest : BusinessUnitTest
    {
        private readonly UnitService _sut;

        public UnitServiceTest()

        {
            var repasitory = new EFUnitRepasitory(SetupContext);
            var unitOfWork = new EfUnitOfWork(SetupContext);
            var blockRepasitory = new EFBlockRepasitory(SetupContext);
            _sut = new UnitAppService(unitOfWork, repasitory,blockRepasitory);
        }

        [Theory]
        [InlineData(ResidenseType.Empty)]
        [InlineData(ResidenseType.Owner)]
        [InlineData(ResidenseType.Tenant)]
        public void Add_Certain_add_a_unit(ResidenseType type)
        {
            var complex = ComplexFactory.Create();
            var block = BlockFactory.Create(complex);
            DbContext.Save(block);
            var dto = CreateUnitDto(type, block.Id);

            _sut.Add(dto);

            var expected = ReadContext.Set<CMDTDD.Entities.Unit>().Single();
            expected.ResidenseType.Should().Be(type);
            expected.BlockId.Should().Be(dto.BlockId);
            expected.Name.Should().Be(dto.Name);

        }


        [Fact]
        public void Add_Certain_incalid_blockId_exception()
        {
            var invalidBlockId = 0;
            var dto = CreateUnitDto(ResidenseType.Empty, invalidBlockId);
            
            var expexted = ()=> _sut.Add(dto);

            expexted.Should().ThrowExactly<InvalidBlockIdExeption>();
        }
        [Fact]
        public void Add_Certain_Block_is_Full_exception()
        {
            var complex = ComplexFactory.Create();
            var block = BlockFactory.Create(complex, 0);
            DbContext.Save(block);
            var dto = CreateUnitDto(ResidenseType.Empty, block.Id);
            
            var expexted = ()=> _sut.Add(dto);

            expexted.Should().ThrowExactly<BlockIsFullException>();
        }


        private static AddUnitDto CreateUnitDto(ResidenseType type, int blockId)
        {
            return new AddUnitDto()
            {
                Name = "dummy",
                BlockId = blockId,
                ResidenseType = type
            };
        }
    }
}