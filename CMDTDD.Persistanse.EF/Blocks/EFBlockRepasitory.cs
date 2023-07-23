using CMDTDD.Entities;
using CMDTDD.Persistanse.EF;
using Microsoft.EntityFrameworkCore;

namespace CMS.Service.Unit.Test.Blocks;

public class EFBlockRepasitory : BlockRepasitory
{
    private readonly DbSet<Block> _blocks;
    private readonly DbSet<CMDTDD.Entities.Unit> _units;

    public EFBlockRepasitory(EFDataContext context)
    {
        _blocks = context.Set<Block>();
        _units = context.Set<CMDTDD.Entities.Unit>();
    }
    public void Add(Block block)
    {
        _blocks.Add(block);
    }

    public List<GetblockDto> GetAllWithUnitCountDetails()
    {
        var result = _blocks.Select(_ => new GetblockDto
        {
            Name = _.Name,
            UnitCount = _.UnitCount,
            RegisterUnitCount = _.Units.Count,
            ReminaingUnitCount = _.UnitCount - _.Units.Count()
        }).ToList();
        return result;
    }

    public GetSingleBlockDto GetById(int blockId)
    {
        var result =
            _blocks.Where(_ => _.Id == blockId).Select(_ => new GetSingleBlockDto
            {
                Name = _.Name,
                UnitsInBlock = _.Units.Select(u => new UnitsInBlockDto
                {
                    Name = u.Name,
                    Resedent = u.ResidenseType.ToString()
                }).ToList()
            });

        return result.FirstOrDefault();
    }

    public Block FindById(int blockId)
    {
        return 
        _blocks.Find(blockId);
    }

    public void Update(Block block)
    {
        _blocks.Update(block);
    }

    public bool UnitIsExistedByBlockId(int blockId)
    {
        return
            _units.Any(_ => _.BlockId == blockId);
    }

    public bool GetDuplicatedNameById(string dtoName)
    {
        return _blocks.Any(_ => _.Name == dtoName );
    }

    public bool IsExisted(int dtoBlockId)
    {
        return
            _blocks.Any(_ => _.Id == dtoBlockId);
    }

    public int GetRemainigUnitCountById(int dtoBlockId)
    {
        var block = _blocks.First(_ => _.Id == dtoBlockId);
        return block.UnitCount - block.Units.Count();
    }
}