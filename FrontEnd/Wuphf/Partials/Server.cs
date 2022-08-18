namespace Wuphf.Api.Client
{
    public partial class Server
    {
        public bool IsTaken => !IsAvailable;
        public bool IsAvailable => string.IsNullOrEmpty(UserNameLastAcquired);
    }
}
