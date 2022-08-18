using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Wuphf.Data;

namespace Wuphf.Api.Controllers;

public class ODataControllerBase<T> : ODataController 
    where T : class
{
    private AppDbContext? _db;

    protected AppDbContext Db => _db ??= HttpContext.RequestServices.GetRequiredService<AppDbContext>();

    [HttpGet]
    [EnableQuery]
    public IActionResult Get(CancellationToken token)
    {
        return Ok(Db.Set<T>());
    }
}