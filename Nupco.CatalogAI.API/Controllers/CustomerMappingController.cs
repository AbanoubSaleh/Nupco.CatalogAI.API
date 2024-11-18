using INUPCO.Catalog.Application.Features.CustomerMappings.Commands;
using INUPCO.Catalog.Application.Features.CustomerMappings.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Nupco.CatalogAI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerMappingController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerMappingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("template")]
    public async Task<IActionResult> DownloadTemplate()
    {
        var result = await _mediator.Send(new GetMappingTemplateQuery());
        return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "mapping_template.xlsx");
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadMappings(IFormFile file)
    {
        var result = await _mediator.Send(new ProcessMappingFileCommand { File = file });
        
        if (result.Errors.Any())
        {
            return File(
                result.ErrorReport!, 
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 
                "mapping_errors.xlsx"
            );
        }

        return Ok(new { 
            Message = "File processed successfully", 
            SuccessCount = result.SuccessCount 
        });
    }
} 