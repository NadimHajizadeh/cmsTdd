using CMDTDD.Services.Contracts;

namespace CMDTDD.Persistanse.EF;

public class EfUnitOfWork : UnitOfWork
{
    private readonly EFDataContext _context;

    public EfUnitOfWork(EFDataContext context)
    {
        _context = context;
    }
    public void Complete()
    {
        _context.SaveChanges();
    }
}