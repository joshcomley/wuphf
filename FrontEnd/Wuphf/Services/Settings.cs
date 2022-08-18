namespace Wuphf.Services
{
    public class Settings : ISettings
    {
        private const string UserNameKey = "username";

        public string UserName
        {
            get => Preferences.Default.Get<string>(UserNameKey, null);
            set => Preferences.Default.Set(UserNameKey, value);
        }
    }
}
