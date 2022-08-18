namespace Wuphf.Api.Client
{
    public partial class AuditLog
    {
        public string ToUserValue => String.IsNullOrEmpty(ToUserName) ? "[Released]" : ToUserName;
    }
}
