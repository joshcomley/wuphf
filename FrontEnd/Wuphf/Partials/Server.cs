namespace Wuphf.Api.Client
{
    public partial class Server
    {
        public bool IsOccupied => string.IsNullOrEmpty(UserNameLastAcquired);
    }
}
