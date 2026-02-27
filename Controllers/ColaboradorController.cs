
using System.Formats.Tar;
using apiBukLitoprocess.Clases;
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
            var result = await _restClient.GetAsync<string>($"/employees/{id_colaborador}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.GetBaseException().Message);
        }
        var response = new { message = "Webhook received successfully", payload };
        return Ok(response);
    }




}
