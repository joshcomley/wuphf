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
            //Model.Servers.Clear();
            //foreach (var server in servers)
            //{
            //    Model.Servers.Add(server);
            //}
            Dispatcher.Dispatch(() =>
            {
                BindingContext = new ServersViewModel
                {
                    Servers = new ObservableCollection<Server>(new List<Server>
                    {
                        new Server
                        {
                            Name = "Test 1"
                        },
                        new Server
                        {
                            Name = "Test 2"
                        }
                    })
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