using System.ComponentModel.DataAnnotations;

namespace CMS.Service.Unit.Test.Complexes;

public class AddComplexDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = default!;
    public int UnitCount { get; set; }
}