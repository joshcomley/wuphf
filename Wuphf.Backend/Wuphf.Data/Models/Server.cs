﻿namespace Wuphf.Data.Models;

public class Server
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTimeOffset? LastAcquired { get; set; }
}