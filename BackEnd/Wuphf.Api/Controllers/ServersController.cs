using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Wuphf.Data.Models;

namespace Wuphf.Api.Controllers;

public class ServersController : ODataControllerBase<Server>
{
    
}

public class ODataControllerBase<T> : ODataController
{
    [HttpGet]
    [EnableQuery]
    public IActionResult Get(CancellationToken token)
    {
        throw new NotImplementedException();
        // return Ok(_context.Products);
    }
}

public record struct Server1
{
}