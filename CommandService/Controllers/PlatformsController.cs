using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers;

[ApiController]
[Route("api/command/[controller]")]
public class PlatformsController : ControllerBase
{
    public PlatformsController()
    {
        
    }

    [HttpPost]
    public ActionResult TestInboundConnection()
    {
        Console.WriteLine("=--> Inbound POST # Command Service");

        return Ok("Inbound test ok from Platforms Controller");
    }
}