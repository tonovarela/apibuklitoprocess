
using apiBukLitoprocess.Clases;
using Microsoft.AspNetCore.Mvc;
using apiBukLitoprocess.Services;

namespace apiBukLitoprocess.Controllers;

[ApiController]
[Route("api/colaborador")]
public class ColaboradorController : ControllerBase
{
    private readonly ColaboradorService _colaboradorService;
    public ColaboradorController(ColaboradorService colaboradorService)=> _colaboradorService = colaboradorService;
    

    [HttpPost("webhook")]
    public async Task<IActionResult> webhook(WebhookPayload payload)
    {        
            var result = await _colaboradorService.handleEventWebhook(payload.Data);
            
            if (result.IsError)
            {
                Console.WriteLine(result.ErrorMessage);
                return StatusCode(result.StatusCode, result.ErrorMessage);
            }
            return Ok(new { message = "Evento procesado correctamente",
                             colaborador = result.colaborador,
                            evento=payload.Data.EventType 
                            });                                           
    }

    [HttpGet("sync")]
    public async Task<IActionResult> sync()
    {
        try
        {
            var colaboradores = await _colaboradorService.sincronizar();
            return Ok(colaboradores);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.GetBaseException().Message);
            return StatusCode(500, "Internal server error");
        }
             
    }

}
