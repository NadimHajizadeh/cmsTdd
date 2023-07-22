namespace CMDTDD.Entities;

public class Complex
{
    public Complex()
    {
        Blocks = new HashSet<Block>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public int UnitCount { get; set; }
    public HashSet<Block> Blocks { get; set; }
}