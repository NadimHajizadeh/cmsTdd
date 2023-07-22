using System.Reflection.Metadata.Ecma335;

namespace CMDTDD.Entities;

public class Block
{
    public Block()
    {
        Units = new HashSet<Unit>();
    }
    public int Id { get; set; }
    public string  Name { get; set; }
    public int UnitCount { get; set; }
    public int ComplexId { get; set; }
    public Complex Complex { get; set; }
    public HashSet<Unit> Units { get; set; }
}