using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wuphf.Data.Models
{
    public class AuditLog
    {
        public int Id { get; set; }
        public int ServerId { get; set; }
        public Server Server { get; set; } = null!;
        public string? FromUserName { get; set; }
        public string? ToUserName { get; set; }
        public DateTimeOffset DateCreated { get; set; }
    }
}