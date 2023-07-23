using CMDTDD.Entities;
using CMDTDD.Persistanse.EF;
using Microsoft.EntityFrameworkCore;

public class EFUnitRepasitory : UnitRepasitory
{
    private readonly DbSet<Unit> _units;


    public EFUnitRepasitory(EFDataContext context)
    {
        _units = context.Set<Unit>();
    }

    public void Add(Unit unit)
    {
        _units.Add(unit);
    }
}