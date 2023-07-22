namespace CMDTDD.Entities;

public class Unit
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public ResidanseType ResidanseType { get; set; }
    public int BlockId { get; set; }
    
    public Block Block { get; set; } = null!;
}
public enum ResidanseType
{
    Empty = 0,
    Owner = 1,
    Tenant = 2
}