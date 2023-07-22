using CMDTDD.Entities;

namespace CMS.Service.Unit.Test.Factory;

public static class BlockFactory
{
    public static Block Create(Complex complex,int? unitCOunt=null,string? name=null)
    {
        return
            new Block()
            {
                Name = name ?? "dummy",
                UnitCount = unitCOunt??50,
                Complex = complex
            };
    }
}