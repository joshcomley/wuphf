using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Wuphf.Data;
using Wuphf.Data.Models;

namespace Wuphf.Api.Controllers;

public class ServersController : ODataControllerBase<Server>
{
    public IHubContext<ApiHub> Hub { get; }
    public AppDbContext Db { get; }

    public ServersController(IHubContext<ApiHub> hub, AppDbContext db)
    {
        Hub = hub;
        Db = db;
    }
    
    [HttpPost]
    public async Task<IActionResult> Take([FromODataUri]int key, 
        [FromBody]ODataActionParameters parameters)
    {
        var server = await Db.Servers.SingleAsync(_ => _.Id == key);
        var userName = (string)parameters["userName"];
        var now = DateTimeOffset.UtcNow;
        var auditLog = new AuditLog
        {
            ServerId = key,
            FromUserName = server.UserNameLastAcquired,
            ToUserName = userName,
            DateCreated = now
        };
        await Db.AuditLogs.AddAsync(auditLog);
        server.DateLastAcquired = now;
        server.UserNameLastAcquired = userName;
        await Db.SaveChangesAsync();
        await Hub.Clients.All.SendCoreAsync("update", new object?[] {server});
        return Ok();
    }
}