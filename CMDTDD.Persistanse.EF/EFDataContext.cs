using Microsoft.EntityFrameworkCore;

namespace CMDTDD.Persistanse.EF;

public class EFDataContext : DbContext
{
    public EFDataContext(DbContextOptions<EFDataContext> option) : base(option)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder model)
    {
        base.OnModelCreating(model);
        model.ApplyConfigurationsFromAssembly(typeof(EFDataContext).Assembly);
    }
}