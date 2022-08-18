using Microsoft.Maui.Controls;
using Wuphf.Api.Client;
using Wuphf.Services;

namespace Wuphf;

public partial class ServersPage : ContentPage
{
    public ServersViewModel Model => BindingContext as ServersViewModel;
    public ServersPage()
    {
        InitializeComponent();
        BindingContext = new ServersViewModel
        {
            UserName = ServiceProvider.GetService<ISettings>().UserName
        };
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
            await UpdateDataAsync(Model);
        });
    }

    private async Task UpdateDataAsync(ServersViewModel Model)
    {
        var api = new WuphfApi(new HttpClient());

        var servers = (await api.Servers_Server_ListServerAsync(null, null, null, $"contains(Name,'{Model.ServerSearch}')", null, null, null, null)).Value;
        Model.Servers = servers.ToList();
        this.Update();
    }

    public ISettings Settings => ServiceProvider.GetService<ISettings>();

    private void UserNameChanged(object sender, TextChangedEventArgs e)
    {
        Settings.UserName = e.NewTextValue;
        Model.UserName = e.NewTextValue;
    }

    private void ServerNameChanged(object sender, TextChangedEventArgs e)
    {
        var model = Model;
        Task.Run(async () =>
        {
            await UpdateDataAsync(model);
        });
    }


    private void OnTakeClick(object sender, EventArgs e)
    {
        var server = (sender as Button).CommandParameter as Server;
        if (string.IsNullOrWhiteSpace(Settings.UserName))
        {
            DisplayAlert("Uh oh", "Please enter your name, first", "Fiiine");
        }
        else
        {
            Task.Run(async () =>
            {
                var api = new WuphfApi(new HttpClient());
                await api.Servers_Server_TakeAsync(server.Id, new Body
                {
                    UserName = Settings.UserName
                });
            });
        }
    }

    private void OnReleaseClick(object sender, EventArgs e)
    {
        var server = (sender as Button).CommandParameter as Server;
        Task.Run(async () =>
        {
            var api = new WuphfApi(new HttpClient());
            await api.Servers_Server_TakeAsync(server.Id, new Body
            {
                UserName = ""
            });
        });
    }
}

public class ServersViewModel
{
    public string ServerSearch { get; set; }
    public string UserName { get; set; }
    public List<Server> Servers { get; set; }
}