using CMDTDD.Entities;

namespace CMS.Service.Unit.Test.Factory;

public static class ComplexFactory
{
    public static Complex Create()
    {
        return
            new Complex()
            {
                Name = "dummy",
                UnitCount = 100,
            };
    }
}