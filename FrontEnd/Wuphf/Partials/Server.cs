namespace Wuphf.Api.Client
{
    internal partial class Server
    {
        public bool IsOccupied => string.IsNullOrEmpty(UserNameLastAcquired);
    }
}
