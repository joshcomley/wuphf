namespace Wuphf.Api.Client
{
    public partial class Server
    {
        public bool IsTaken => !IsAvailable;
        public bool IsAvailable => string.IsNullOrEmpty(UserNameLastAcquired);
        public string BgColor => !IsAvailable && UserNameLastAcquired.Equals("Aliya") ? "#76E109" : "White"; 
    }
}
