using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using WeatherTwentyOne.Services;
using Wuphf.Services;

namespace Wuphf;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
        SetupTrayIcon();

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Task.Run(async () =>
        {
            await ServiceProvider.GetService<IServerNotifications>()
                .StartAsync();
        });
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        //var client = new Api.Client.WuphfApi(new HttpClient());
        //var servers = await client.Servers_Server_ListServerAsync(3, null, search: null, filter: null, null, orderby: null, select: null, expand: null);

        await Navigation.PushAsync(new ServersPage());
        //count++;

        //if (count == 1)
        //	CounterBtn.Text = $"Clicked {count} time";
        //else
        //	CounterBtn.Text = $"Clicked {count} times";

        //SemanticScreenReader.Announce(CounterBtn.Text);
    }

    private void SetupTrayIcon()
    {
        var trayService = ServiceProvider.GetService<ITrayService>();

        if (trayService != null)
        {
            trayService.Initialize();
            trayService.ClickHandler = () =>
                ServiceProvider.GetService<INotificationService>()
                    ?.ShowNotification("Hello Build! 😻 From .NET MAUI", "How's your weather?  It's sunny where we are 🌞");
        }
    }
}

