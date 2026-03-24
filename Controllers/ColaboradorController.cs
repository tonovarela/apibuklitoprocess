
using apiBukLitoprocess.Clases;
using Microsoft.AspNetCore.Mvc;
using apiBukLitoprocess.Services;

namespace apiBukLitoprocess.Controllers;

[ApiController]
[Route("api/colaborador")]
public class ColaboradorController : ControllerBase
{
    private readonly ColaboradorService _colaboradorService;
    public ColaboradorController(ColaboradorService colaboradorService) => _colaboradorService = colaboradorService;


    [HttpPost("webhook")]
    public async Task<IActionResult> webhook(WebhookPayload payload)
    {
        var result = await _colaboradorService.handleEventWebhook(payload.Data);
        if (result.IsError)
        {
            return StatusCode(result.StatusCode, new
            {
                success = false,
                statusCode = result.StatusCode,
                message = result.ErrorMessage
            });
        }

        return Ok(new
        {
            success = true,
            statusCode = 200,
            data = result.colaborador
        });
    }

    [HttpGet("sync")]
    public async Task<IActionResult> sync()
    {
        try
        {
            var colaboradores = await _colaboradorService.Sincronizar();
            return Ok(new
            {
                success = true,
                statusCode = 200,
                data = colaboradores
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.GetBaseException().Message);
            return StatusCode(500, new
            {
                success = false,
                statusCode = 500,
                message = "Internal server error"
            });
        }

    }

}
