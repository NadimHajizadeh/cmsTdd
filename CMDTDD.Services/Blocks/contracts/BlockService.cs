using CMDTDD.Entities;
using CMDTDD.Services.Complexes.Contracts.Dto;

namespace CMS.Service.Unit.Test.Blocks;

public interface BlockService
{
    void Add(AddBlockDto dto);


    List<GetblockDto> GetAllWithUnitCountDetails();
    GetSingleBlockDto GetById(int blockId);
    void Update(int blockId, UpdateBlockDto dto);
}