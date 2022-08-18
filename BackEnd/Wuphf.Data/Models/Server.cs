using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wuphf.Data.Models
{
    public class Server
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? UserNameLastAcquired { get; set; }
        public DateTimeOffset? DateLastAcquired { get; set; }
    }
}

