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

        var block = new Block()
        {
            Name = dto.Name,
            UnitCount = dto.UnitCount,
            ComplexId = dto.ComplexId
        };

        _repasitory.Add(block);
        _unitOfWork.Complete();
    }
}