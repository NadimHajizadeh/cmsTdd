using System.ComponentModel.DataAnnotations;
using CMDTDD.Entities;

namespace CMS.Service.Unit.Test.Units;

public class AddUnitDto

{
    [Required] [MaxLength(30)] public string Name { get; set; }

    [Required] public ResidenseType ResidenseType { get; set; }

    public int BlockId { get; set; }
}