using CMDTDD.Services.Complexes.Contracts.Dto;

namespace CMS.Service.Unit.Test.Complexes;

public interface ComplexService
{
    void Add(AddComplexDto dto);

    List<GetAllComplexesWithUnitCountDeataiDto>
        GetAllComplexesWithUnitCountDeatail();

    List<GetAllComplexWithBlockDetaisDto> GetAllComplexWithBlockDetails(string?
        blockName = null);

    List<GetAllUsageTypesOfComplexDto> GetAllUsageTypesById(int complexId);

    void Update(int id, UpdateComplexDto dto);
    void Delete(int complexId);
    GetOneComplexWithBlockAndUnitcountDeatilAndDto GetOne(int id );
}