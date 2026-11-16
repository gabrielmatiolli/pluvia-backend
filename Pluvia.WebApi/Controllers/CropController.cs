using Microsoft.AspNetCore.Mvc;
using Pluvia.Domain.UseCases.Crop.Inputs;
using Pluvia.Domain.UseCases.Crop.Interfaces;
using Pluvia.WebApi.Contracts.Crop;

namespace Pluvia.WebApi.Controllers;

[ApiController]
[Route("api/crops")]
public sealed class CropController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCropsAsync(
        [FromServices] IListCropsUseCase useCase,
        CancellationToken ct)
    {
        var output = await useCase.ExecuteAsync(new ListCropsInput(), ct);
        var response = output.Crops
            .Select(crop => new CropResponse(crop.Id, crop.Type))
            .ToList();

        return Ok(response);
    }
}
