using INUPCO.Catalog.Application.Common.Interfaces;
using INUPCO.Catalog.Application.Features.Manufacturers.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Nupco.CatalogAI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ManufacturerController : ControllerBase
{
    private readonly IMediator _mediator;

    public ManufacturerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadManufacturers(IFormFile file)
    {
        var result = await _mediator.Send(new ProcessManufacturerFileCommand { File = file });
        
        if (result.Errors.Any())
        {
            return File(
                result.ErrorReport!, 
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 
                "manufacturer_errors.xlsx"
            );
        }

        return Ok(new { 
            Message = "File processed successfully", 
            ProcessedCount = result.ProcessedCount 
        });
    }

    [HttpGet("template")]
    public IActionResult GetTemplate([FromServices] IExcelProcessor excelProcessor)
    {
        var template = excelProcessor.GenerateManufacturerTemplate();
        
        return File(
            template, 
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "manufacturer_template.xlsx"
        );
    }
} 