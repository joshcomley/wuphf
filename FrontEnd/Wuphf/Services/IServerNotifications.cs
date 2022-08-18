using Wuphf.Api.Client;

namespace Wuphf.Services;

public interface IServerNotifications
{
    Task StartAsync();
    void OnUpdate(Action<Server> action);
    void OnUpdate(Func<Server, Task> action);
}