using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wuphf.Data.Models
{
    [Table("AuditLogs")]
    public  class AuditLog
    {
        public int Id { get; set; }
        public int ServerId { get; set; }
        [MaxLength(50)]
        public string From { get; set; }
        [MaxLength(50)]
        public string To { get; set; }
        public DateTimeOffset DateCreated { get; set; }
    }
}
