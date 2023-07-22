using CMDTDD.Entities;

namespace CMS.Service.Unit.Test.Factory;

public static class UnitFatory
{
    public static CMDTDD.Entities.Unit Create(Block block)
    {
        return
            new CMDTDD.Entities.Unit()
            {
                Name = "dummy",
                Block = block,
                ResidanseType = ResidanseType.Owner,
            };
    }
}