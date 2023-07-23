using CMDTDD.Entities;
using CMDTDD.Services.Complexes.Contracts;
using CMDTDD.Services.Complexes.Contracts.Dto;
using CMDTDD.Services.Complexes.Exceptions;
using CMDTDD.Services.Contracts;

namespace CMS.Service.Unit.Test.Complexes;

public class ComplexAppService : ComplexService
{
    private readonly ComplexRepasitory _repasitory;
    private readonly UnitOfWork _unitOfWork;

    public ComplexAppService(ComplexRepasitory repasitory,
        UnitOfWork unitOfWork)
    {
        _repasitory = repasitory;
        _unitOfWork = unitOfWork;
    }

    public void Add(AddComplexDto dto)
    {
        var complex = new Complex()
        {
            Name = dto.Name,
            UnitCount = dto.UnitCount
        };
        _repasitory.Add(complex);
        _unitOfWork.Complete();
    }

    public List<GetAllComplexesWithUnitCountDeataiDto>
        GetAllComplexesWithUnitCountDeatail()
    {
        return
            _repasitory.GetAllComplexesWithUnitCountDeatail();
    }

    public List<GetAllComplexWithBlockDetaisDto> GetAllComplexWithBlockDetails
        (string? blockName = null)
    {
        return
            _repasitory.GetAllComplexWithBlockDetails(blockName);
    }

    public List<GetAllUsageTypesOfComplexDto> GetAllUsageTypesById(int complexId)
    {
        var isExisted = _repasitory.IsExistById(complexId);
        if (!isExisted)
        {
            throw new InvalidComplexIdExeption();
        }
        return
            _repasitory.GetAllUsageTypesByComplexId(complexId);
    }

    public void Update(int id, UpdateComplexDto dto)
    {
        var complex = _repasitory.FindeById(id);
        if (complex is null)
        {
            throw new InvalidComplexIdExeption();
        }
        

        var hasUnits = _repasitory.HasUnitsById(id);
        if (hasUnits)
        {
            throw new ComplexHasUnitException();
        }
        complex.UnitCount = dto.UnitCount;

        _repasitory.Update(complex);
        _unitOfWork.Complete();
    }

    public void Delete(int complexId)
    {
        var complex = _repasitory.FindeById(complexId);
        if (complex is null)
        {
            throw new InvalidComplexIdExeption();
        }
        var hasUnits = _repasitory.HasUnitsById(complexId);
        if (hasUnits)
        {
            throw new ComplexHasUnitException();
        }
        _repasitory.Delete(complex);
        _unitOfWork.Complete();
    }

  

    public GetOneComplexWithBlockAndUnitcountDeatilAndDto GetOne(int id)
    {
        return 
        _repasitory.GetOne(id);
    }
}