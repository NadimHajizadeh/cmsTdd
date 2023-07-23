using CMDTDD.Entities;

namespace CMS.Service.Unit.Test.Factory;

public static class UnitFatory
{
    public static CMDTDD.Entities.Unit Create(Block block,ResidenseType? type=null)
    {
        return
            new CMDTDD.Entities.Unit()
            {
                Name = "dummy",
                Block = block,
                ResidenseType = type ?? ResidenseType.Owner
            };
    }
}