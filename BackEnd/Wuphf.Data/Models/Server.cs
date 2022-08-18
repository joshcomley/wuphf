using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wuphf.Data.Models
{
    [Table("Servers")]
    public class Server
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string ServerName { get; set; }
        [MaxLength(50)]
        public string? Username { get; set; }
        public DateTimeOffset? LastAcquired { get; set; }
    }
}

