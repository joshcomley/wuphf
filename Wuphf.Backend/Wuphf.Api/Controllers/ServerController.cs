using Microsoft.AspNetCore.Mvc;
using Wuphf.Data.Models;

namespace Wuphf.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServerController : Controller
    {
        private readonly ILogger<ServerController> _logger;

        public ServerController(ILogger<ServerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Server> GetAllServers()
        {
            return new List<Server>
            {
                new Server{Id = 1, Name = "Server1", LastAcquired = DateTimeOffset.Now},
                new Server{Id = 2, Name = "Server2", LastAcquired = DateTimeOffset.Now},
                new Server{Id = 3, Name = "Server3", LastAcquired = DateTimeOffset.Now},
            };
        }
    }
}
