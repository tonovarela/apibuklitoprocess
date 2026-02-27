
using apiBukLitoprocess.Clases;
using Microsoft.AspNetCore.Mvc;

namespace apiBukLitoprocess.Controllers;

[ApiController]
[Route("api/colaborador")]
public class ColaboradorController : ControllerBase
{
     [HttpPost("webhook")]
    public IActionResult webhook(WebhookPayload payload )
    {    
        Console.WriteLine("Webhook received with payload: ");
        Console.WriteLine(payload.ToString());
        var response = new { message = "Webhook received successfully",  payload };
        return Ok(response);
    }
    



}
