using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
        });
    }

    public async void OnServerClick(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}

public class AuditLogViewModel : INotifyPropertyChanged
{
    private ICollection<Api.Client.AuditLog> _auditLogs;

    public ICollection<Api.Client.AuditLog> AuditLogs
    {
        get => _auditLogs;
        set
        {
            if (Equals(value, _auditLogs)) return;
            _auditLogs = value;
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
