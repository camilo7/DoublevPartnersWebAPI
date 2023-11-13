using System;
using System.Collections.Generic;

namespace DB.Models;

public partial class User
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public byte[]? Pass { get; set; }

    public DateTime? CreationDate { get; set; }
}
