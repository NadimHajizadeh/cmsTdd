using System.ComponentModel.DataAnnotations;

namespace CMS.Service.Unit.Test.Blocks;

public class AddBlockDto
{
    [Required]
    public string Name { get; set; } = null!;

    public int UnitCount { get; set; }
    public int ComplexId { get; set; }
}