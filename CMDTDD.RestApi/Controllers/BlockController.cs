using CMDTDD.Services.Complexes.Contracts.Dto;
using CMS.Service.Unit.Test.Blocks;
using Microsoft.AspNetCore.Mvc;

namespace CMDTDD.RestApi.Controllers;
[Route("blocks")]
public class BlockController : Controller
{
    private readonly BlockService _service;

    public BlockController(BlockService service)
    {
        _service = service;
    }

    [HttpPost]
    public void Add([FromBody]AddBlockDto dto)
    {
        _service.Add(dto);
    }

    [HttpGet]
    public List<GetblockDto> GetAll()
    {
        return
            _service.GetAllWithUnitCountDetails();
    }

    [HttpGet("{id}")]
    public GetSingleBlockDto GetById([FromRoute] int id)
    {
        return
            _service.GetById(id);
    }

    [HttpPut("{id}")]
    public void Update([FromRoute] int id, [FromBody] UpdateBlockDto dto)
    {
        _service.Update(id,dto);
    }

}