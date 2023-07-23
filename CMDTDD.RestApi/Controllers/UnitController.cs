using CMS.Service.Unit.Test.Units;
using Microsoft.AspNetCore.Mvc;

namespace CMDTDD.RestApi.Controllers;

[Route("units")]
public class UnitController : Controller
{
    private readonly UnitService _service;

    public UnitController(UnitService service )
    {
        _service = service;
    }

    [HttpPost]
    public void Add(AddUnitDto dto)
    {
        _service.Add(dto);
    }
    
    
    
}