using System.ComponentModel;
using System.Runtime.CompilerServices;
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

            Model.Servers = Model.Servers.ToList();
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

    private async Task UpdateDataAsync(ServersViewModel model)
    {
        var api = new WuphfApi(new HttpClient());
        var servers = (await api.Servers_Server_ListServerAsync(null, null, null, $"contains(Name,'{model.ServerSearch}')", null, null, null, null)).Value;
        model.Servers = servers.ToList();
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
                    ByUserName = Settings.UserName,
                    UserName = Settings.UserName
                });
            });
        }
    }

    private async void OnReleaseClick(object sender, EventArgs e)
    {
        var server = (sender as Button).CommandParameter as Server;
        var canContinue = server.UserNameLastAcquired?.Trim() == Settings.UserName?.Trim();
        if (!canContinue)
        {
            canContinue = await DisplayAlert("You sure, guv?", @$"This is taken by ""{server.UserNameLastAcquired}"", are you sure you want to release it?", "Yes", "No");
        }

        if (canContinue)
        {
#pragma warning disable CS4014
            Task.Run(async () =>
#pragma warning restore CS4014
            {
                var api = new WuphfApi(new HttpClient());
                await api.Servers_Server_TakeAsync(server.Id, new Body
                {
                    ByUserName = Settings.UserName,
                    UserName = ""
                });
            });
        }
    }

    public string UserName => ServiceProvider.GetService<ISettings>().UserName;
}

public class ServersViewModel : INotifyPropertyChanged
{
    private List<Server> _servers;
    public string ServerSearch { get; set; }
    public string UserName { get; set; }

    public List<Server> Servers
    {
        get => _servers;
        set
        {
            if (Equals(value, _servers)) return;
            _servers = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}