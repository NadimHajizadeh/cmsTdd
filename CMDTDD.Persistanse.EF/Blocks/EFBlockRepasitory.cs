using CMDTDD.Entities;
using CMDTDD.Persistanse.EF;
using Microsoft.EntityFrameworkCore;

namespace CMS.Service.Unit.Test.Blocks;

public class EFBlockRepasitory : BlockRepasitory
{
    private readonly DbSet<Block> _blocks;

    public EFBlockRepasitory(EFDataContext context)
    {
        _blocks = context.Set<Block>();
    }
    public void Add(Block block)
    {
        _blocks.Add(block);
    }
}