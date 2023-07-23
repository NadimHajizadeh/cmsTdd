using CMDTDD.Entities;

namespace CMS.Service.Unit.Test.Blocks;

public class GetSingleBlockDto
{
    
    public string Name { get; set; }
    public List<UnitsInBlockDto> UnitsInBlock { get; set; } = new();
}

public class UnitsInBlockDto
{
    public string Name { get; set; }
    public string Resedent { get; set; }
}