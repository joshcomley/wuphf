using System.Collections.ObjectModel;
using Wuphf.Api.Client;

namespace Wuphf;

public partial class ServersPage : ContentPage
{
    public ServersViewModel Model => BindingContext as ServersViewModel;
    public ServersPage()
    {
        InitializeComponent();
        BindingContext = new ServersViewModel();
        Task.Run(async () =>
        {
            var api = new WuphfApi(new HttpClient());
            var servers = (await api.Servers_Server_ListServerAsync(null, null, null, null, null, null, null, null)).Value;
            Dispatcher.Dispatch(() =>
            {
                BindingContext = new ServersViewModel
                {
                    Servers = new ObservableCollection<Server>(servers.ToList()) 
                };
            });
        });
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}

public class ServersViewModel
{
    public ObservableCollection<Server> Servers { get; set; } = new();
}