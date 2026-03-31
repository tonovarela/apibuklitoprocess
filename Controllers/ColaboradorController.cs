
using apiBukLitoprocess.Clases;
using Microsoft.AspNetCore.Mvc;
using apiBukLitoprocess.Services;

namespace apiBukLitoprocess.Controllers;

[ApiController]
[Route("api/colaborador")]
public class ColaboradorController : ControllerBase
{
    private readonly ColaboradorService _colaboradorService;
    private readonly AsistenciaService _asistenciaService;
    public ColaboradorController(ColaboradorService colaboradorService, AsistenciaService asistenciaService)
    {
        _colaboradorService = colaboradorService;
        _asistenciaService = asistenciaService;
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> webhook(WebhookPayload payload)
    {        
        var clientIp = HttpContext.Connection.RemoteIpAddress?.ToString();
        var forwardedFor = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        var origin = HttpContext.Request.Headers["Origin"].FirstOrDefault();
        var referer = HttpContext.Request.Headers["Referer"].FirstOrDefault();
        var userAgent = HttpContext.Request.Headers["User-Agent"].FirstOrDefault();
        var ip = forwardedFor ?? clientIp;

        // DNS inverso para obtener el hostname del servidor que hace la petición
        string? hostname = null;
        try
        {
            if (System.Net.IPAddress.TryParse(ip, out var parsedIp))
            {
                var hostEntry = await System.Net.Dns.GetHostEntryAsync(parsedIp);
                hostname = hostEntry.HostName;
            }
        }
        catch { /* DNS inverso no disponible */ }

        Console.WriteLine($"Webhook request from IP: {ip} | Hostname: {hostname ?? "N/A"} | Origin: {origin ?? "N/A"} | Referer: {referer ?? "N/A"} | User-Agent: {userAgent ?? "N/A"}");
        Console.WriteLine($"Received webhook event:for Employee ID: {payload.Data.EmployeeId}");
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



    [HttpGet("checadas")]
    public async Task<IActionResult> GetChecadas()
    {
        try
        {           
            var checadas = await _asistenciaService.RegistroAsistencias(DateOnly.FromDateTime(DateTime.Now.AddDays(-30)));            
            return Ok(new{success = true,statusCode = 200,data = checadas});
        }
        catch (Exception e)
        {
            Console.WriteLine(e.GetBaseException().Message);
            return StatusCode(500, new{success = false,statusCode = 500,message = "Internal server error"
            });
        }
    }
    

}
