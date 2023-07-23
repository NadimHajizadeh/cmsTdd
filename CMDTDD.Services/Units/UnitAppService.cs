using CMDTDD.Entities;
using CMDTDD.Services.Contracts;
using CMS.Service.Unit.Test.Blocks;
using CMS.Service.Unit.Test.Units;

public class UnitAppService : UnitService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly UnitRepasitory _repasitory;
    private readonly BlockRepasitory _blockRepasitoryrepasitory;

    public UnitAppService(UnitOfWork unitOfWork, UnitRepasitory repasitory,BlockRepasitory blockRepasitoryrepasitory)
    {
        _unitOfWork = unitOfWork;
        _repasitory = repasitory;
        _blockRepasitoryrepasitory = blockRepasitoryrepasitory;
      
    }

    public void Add(AddUnitDto dto)
    {

        var isExisted = _blockRepasitoryrepasitory.IsExisted(dto.BlockId);
        if (!isExisted)
        {
            throw new InvalidBlockIdExeption();
        }

        var blockRemaingUniCout = _blockRepasitoryrepasitory.GetRemainigUnitCountById(dto.BlockId);
        
        if (blockRemaingUniCout<1)
        {
            throw new BlockIsFullException();
        }
        
        
        var unit = new Unit()
        {
            Name = dto.Name,
            ResidenseType = dto.ResidenseType,
            BlockId = dto.BlockId,
        };
        _repasitory.Add(unit);
        _unitOfWork.Complete();
    }
}