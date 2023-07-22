namespace CMDTDD.Services.Complexes.Contracts.Dto;

public class GetAllComplexWithBlockDetaisDto
{
    public GetAllComplexWithBlockDetaisDto()
    {
        Block = new();
    }

    public string Name { get; set; }
    public List<ComplexBlockDto> Block { get; set; }
}

public class ComplexBlockDto
{
    public string BlockName { get; set; }
    public int BlockUnitCount { get; set; }

    public int RemainingUnitCount { get; set; }
}
