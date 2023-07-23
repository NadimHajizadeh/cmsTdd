using CMDTDD.Entities;
using CMDTDD.Services.Complexes.Contracts;
using CMDTDD.Services.Complexes.Contracts.Dto;
using CMDTDD.Services.Complexes.Exceptions;
using CMDTDD.Services.Contracts;
using CMS.Service.Unit.Test.Complexes;

namespace CMS.Service.Unit.Test.Blocks;

public class BlockAppService : BlockService
{
    private readonly BlockRepasitory _repasitory;
    private readonly UnitOfWork _unitOfWork;
    private readonly ComplexRepasitory _complexRepasitory;

    public BlockAppService(BlockRepasitory repasitory,
        UnitOfWork unitOfWork,
        ComplexRepasitory complexRepasitory)
    {
        _repasitory = repasitory;
        _unitOfWork = unitOfWork;
        _complexRepasitory = complexRepasitory;
    }

    public void Add(AddBlockDto dto)
    {
        var isExist = _complexRepasitory.IsExistById(dto.ComplexId);
        if (!isExist)
        {
            throw new InvalidComplexIdExeption();
        }

        var complexUnitCount = _complexRepasitory.GetUnitCount(dto.ComplexId);
        if (dto.UnitCount > complexUnitCount)
        {
            throw new InvalidUnitCountException();
        }

        var usedBlockUnittIncomplex = _complexRepasitory.GetLeftUnitCounts(dto.ComplexId);
        var letfUnit = complexUnitCount - usedBlockUnittIncomplex;
        if (letfUnit<dto.UnitCount)
        {
            throw new ComplexIsFullException();
        }

        
        var block = new Block()
        {
            Name = dto.Name,
            UnitCount = dto.UnitCount,
            ComplexId = dto.ComplexId
        };

        _repasitory.Add(block);
        _unitOfWork.Complete();
    }

    public List<GetblockDto> GetAllWithUnitCountDetails()
    {
        return
            _repasitory.GetAllWithUnitCountDetails();
    }

    public GetSingleBlockDto GetById(int blockId)
    {
        return
            _repasitory.GetById(blockId);
    }

    public void Update(int blockId, UpdateBlockDto dto)
    {
        var block = _repasitory.FindById(blockId);
        if (block is null)
        {
            throw new InvalidBlockIdExeption();
        }

        var complexId = block.ComplexId;
        var unitIsExisted = _repasitory
            .UnitIsExistedByBlockId(block.Id);

        if (unitIsExisted)
        {
            throw new blockHasUnitsExeption();
        }
        var isDuplicatedName = _repasitory
            .GetDuplicatedNameById(dto.Name);
        if (isDuplicatedName)
        {
            throw new BlockDuplicatedNameException();
        }

        var leftUnitCount = _complexRepasitory.GetLeftUnitCounts(complexId);
        if (leftUnitCount<dto.UnitCount)
        {
            throw new ComplexIsFullException();
        }
        
       
        
        block.UnitCount = dto.UnitCount;
        block.Name = dto.Name;

        _repasitory.Update(block);
        _unitOfWork.Complete();

    }
}