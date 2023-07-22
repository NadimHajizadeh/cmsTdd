using CMDTDD.Services.Complexes.Contracts.Dto;
using CMS.Service.Unit.Test.Complexes;
using Microsoft.AspNetCore.Mvc;

namespace CMDTDD.RestApi.Controllers;
[Route("complexes")]
public class ComplexControler : Controller
{
    private readonly ComplexService _service;

    public ComplexControler(ComplexService service)
    {
        _service = service;
    }

    [HttpPost]
    public void Add([FromBody]AddComplexDto dto)
    {
        _service.Add(dto);
    }
    [HttpGet]
    public List<GetAllComplexesWithUnitCountDeataiDto>
        GetAllComplexesWithUnitCountDeatail()
    {
        return
            _service.GetAllComplexesWithUnitCountDeatail();
    }

    [HttpGet("with-block-detail")]
    public List<GetAllComplexWithBlockDetaisDto> GetAllWithBloclDetails(string? blockName)
    {
        return
            _service.GetAllComplexWithBlockDetails(blockName);
    }
    
    [HttpGet("{id}/all-usage-types")]
    public List<GetAllUsageTypesOfComplexDto> GetAllUsageTypes([FromRoute] int id)
    {
        return _service.GetAllUsageTypesById(id);
    }
    
    [HttpPatch("{id}")]
    
    public void UnitCount([FromRoute] int id,
        [FromBody] UpdateComplexDto dto)
    {
        _service.Update(id, dto);
    }

    [HttpDelete("{id}")]
    public void Delete([FromRoute] int id)
    {
        _service.Delete(id);
    }
    
}