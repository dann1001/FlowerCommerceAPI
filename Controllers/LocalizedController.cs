using Microsoft.Extensions.Localization;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    private readonly IStringLocalizer _localizer;

    public HomeController(IStringLocalizer<SharedResource> localizer)
    {
        _localizer = localizer;
    }

    [HttpGet("welcome")]
    public IActionResult GetWelcomeMessage()
    {
        var message = _localizer["WelcomeMessage"];
        return Ok(new { Message = message });
    }
}
