
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Formats.Tar;
using apiBukLitoprocess.Clases;
using apiBukLitoprocess.responseApi;

using Microsoft.AspNetCore.Mvc;

namespace apiBukLitoprocess.Controllers;

[ApiController]
[Route("api/colaborador")]
public class ColaboradorController : ControllerBase
{
   
    private readonly RestClientService _restClient;
    public ColaboradorController(RestClientService restClient)
    {
        _restClient = restClient;
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> webhook(WebhookPayload payload)
    {
        try
        {
            int id_colaborador = payload.Data.EmployeeId;
            var result = await _restClient.GetAsync<ResponseColaborador>($"/employees/{id_colaborador}");                        
        }
        catch (Exception e)
        {
            Console.WriteLine(e.GetBaseException().Message);
        }
        var response = new { message = "Webhook received successfully", payload };
        return Ok(response);
    }

    [HttpGet("sync")]
    public async Task<IActionResult> Sync()
    {
        try
        {
            var response = await _restClient.GetAsync<ResponseListColaborador>("/employees");
            Console.WriteLine(response);
            return Ok(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.GetBaseException().Message);
            return StatusCode(500, "Internal server error");
        }
     
    }

}
