using CMDTDD.Entities;
using CMDTDD.Services.Complexes.Contracts.Dto;

namespace CMDTDD.Services.Complexes.Contracts;

public interface ComplexRepasitory
{
    void Add(Complex complex);
    bool IsExistById(int complexId);
    List<GetAllComplexesWithUnitCountDeataiDto> GetAllComplexesWithUnitCountDeatail();
    List<GetAllComplexWithBlockDetaisDto> GetAllComplexWithBlockDetails(string? blockName);
    List<GetAllUsageTypesOfComplexDto> GetAllUsageTypesByComplexId(int complexId);


    Complex FindeById(int id);
    void Update(Complex complex);
    bool HasUnitsById(int complexId);
    void Delete(Complex complex);
    int GetUnitCount(int complexId);
}