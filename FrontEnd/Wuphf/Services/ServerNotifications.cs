using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Wuphf.Api.Client;

namespace Wuphf.Services
{
    public class ServerNotifications : IServerNotifications
    {
        private List<Func<Server, Task>> _actions = new List<Func<Server, Task>>();
        public ServerNotifications()
        {
            Connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/hub")
                .Build();
        }

        public HubConnection Connection { get; set; }

        private bool _started = false;
        public async Task StartAsync()
        {
            if (_started)
            {
                return;
            }

            _started = true;
            Connection.On("update", new[] { typeof(Server) }, async (message) =>
            {
                foreach (var action in _actions)
                {
                    await action(message[0] as Server);
                }
            });
            await Connection.StartAsync();
        }

        public void OnUpdate(Action<Server> action)
        {
            OnUpdate(_ =>
            {
                action(_);
                return Task.CompletedTask;
            });
        }

        public void OnUpdate(Func<Server, Task> action)
        {
            _actions.Add(action);
        }
    }
}
