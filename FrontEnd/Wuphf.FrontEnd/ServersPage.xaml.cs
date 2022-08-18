using Wuphf.Api.Client;

namespace Wuphf;

public partial class ServersPage : ContentPage
{
    public ServersViewModel Model => BindingContext as ServersViewModel;
    public ServersPage()
    {
        InitializeComponent();
        BindingContext = new ServersViewModel();
    }

    public async void OnAuditClick(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AuditLog());
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Task.Run(async () =>
        {
            var api = new WuphfApi(new HttpClient());
            var servers = (await api.Servers_Server_ListServerAsync(null, null, null, null, null, null, null, null)).Value;
            Model.Servers = servers;
            this.Update();
        });
    }
}

public class ServersViewModel
{
    public ICollection<Server> Servers { get; set; }
}