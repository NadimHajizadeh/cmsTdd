using CMDTDD.Entities;

namespace CMS.Service.Unit.Test.Blocks;

public class UnitToAddBlockDto
{
    public string Name { get; set; }
    public ResidenseType ResidenseType { get; set; }
    public Block Block { get; set; }
}