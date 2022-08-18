using System.Collections.Generic;
using Wuphf.Api.Client;

namespace Wuphf;

public partial class AuditLog : ContentPage
{
    public AuditLogViewModel Model => BindingContext as AuditLogViewModel;
    public AuditLog()
	{
		InitializeComponent();
        BindingContext = new AuditLogViewModel();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Task.Run(async () =>
        {
            var api = new WuphfApi(new HttpClient());
            var audits = (await api.AuditLogs_AuditLog_ListAuditLogAsync(null, null, null, null, null, new List<Anonymous>() { Anonymous.DateCreated_desc } , null, new List<Anonymous3>() { Anonymous3.Server })).Value;
            Model.AuditLogs = audits;
            this.Update();
        });
    }

    public async void OnServerClick(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}

public class AuditLogViewModel
{
    public ICollection<Api.Client.AuditLog> AuditLogs { get; set; }
}
