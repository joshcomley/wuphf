using Wuphf.Api.Client;
using Wuphf.Services;

namespace Wuphf;

public partial class ServersPage : ContentPage
{
    public ServersViewModel Model => BindingContext as ServersViewModel;
    public ServersPage()
    {
        InitializeComponent();
        BindingContext = new ServersViewModel();
        ServerNotifications = ServiceProvider.GetService<IServerNotifications>();
        ServerNotifications.OnUpdate(_ =>
        {
            var firstOrDefault = Model.Servers.FirstOrDefault(s => s.Id == _.Id);
            if (firstOrDefault == null)
            {
                Model.Servers.Add(_);
            }
            else
            {
                var index = Model.Servers.IndexOf(firstOrDefault);
                Model.Servers[index] = _;
            }
            this.Update();
        });
    }

    public IServerNotifications ServerNotifications { get; set; }

    public async void OnAuditClick(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AuditLog());
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Task.Run(async () =>
        {
            await ServerNotifications.StartAsync();
            var api = new WuphfApi(new HttpClient());

            var servers = (await api.Servers_Server_ListServerAsync(null, null, null, null, null, null, null, null)).Value;
            Model.Servers = servers.ToList();
            this.Update();
        });
    }

    private void OnServerClick(object sender, EventArgs e)
    {
        var server = (sender as Button).CommandParameter as Server;
        Task.Run(async () =>
        {
            var api = new WuphfApi(new HttpClient());
            await api.Servers_Server_TakeAsync(server.Id, new Body
            {
                UserName = "Josh"
            });
        });

    }
}

public class ServersViewModel
{
    public List<Server> Servers { get; set; }
}