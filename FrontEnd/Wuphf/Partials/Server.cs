using System.Globalization;

namespace Wuphf.Api.Client
{
    public partial class Server
    {
        public bool IsTaken => !IsAvailable;
        public bool IsAvailable => string.IsNullOrEmpty(UserNameLastAcquired);
        public Server Self => this;
    }

    public class BgColorConverter : BindableObject, IValueConverter
    {
        public static readonly BindableProperty UserNameProperty = BindableProperty.Create(nameof(UserName), typeof(string), typeof(BgColorConverter), null);
        public string UserName
        {
            get => (string)GetValue(UserNameProperty);
            set => SetValue(UserNameProperty, value);
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var server = value as Server;
            
            return !server.IsAvailable && server.UserNameLastAcquired.Equals(UserName) ? Color.FromArgb("#2276E109") : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
