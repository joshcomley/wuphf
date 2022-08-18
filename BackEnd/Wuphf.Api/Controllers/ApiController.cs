using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Wuphf.Api.Controllers;

[Route("{controller}")]
public class ApiController : Controller
{
    public IHubContext<ApiHub> Context { get; }

    public ApiController(IHubContext<ApiHub> context)
    {
        Context = context;
    }
    
    [HttpGet("{method}")]
    public async Task<IActionResult> Update()
    {
        await Context.Clients.All.SendCoreAsync("update", new object[] {$"Hello {DateTime.UtcNow}"});
        return Ok("Done");
    }
}