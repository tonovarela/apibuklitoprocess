
using System.Formats.Tar;
using apiBukLitoprocess.Clases;
using apiBukLitoprocess.ResponseApi;
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
            Console.WriteLine("Entrando a la consulta de buk");
            Console.WriteLine(result?.data?.id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.GetBaseException().Message);
        }
        var response = new { message = "Webhook received successfully", payload };
        return Ok(response);
    }




}
