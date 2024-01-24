using Microsoft.AspNetCore.Mvc;

namespace AlfaBetBackendExercise.Controllers;

[Route("[controller]")]
[ApiController]
public class AliveController : ControllerBase
{
    [HttpGet]
    public string Alive()
    {
        return "I'm alive!";
    }
}