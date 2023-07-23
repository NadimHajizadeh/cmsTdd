using CMDTDD.Entities;

namespace CMS.Service.Unit.Test.Blocks;

public interface BlockRepasitory
{
    void Add(Block block);
    List<GetblockDto> GetAllWithUnitCountDetails();
    GetSingleBlockDto GetById(int blockId);
    Block FindById(int blockId);
    void Update(Block block);
    bool UnitIsExistedByBlockId(int blockId);
    bool GetDuplicatedNameById(string dtoName);
    bool IsExisted(int dtoBlockId);
    int GetRemainigUnitCountById(int dtoBlockId);
}