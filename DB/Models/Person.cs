using System;
using System.Collections.Generic;

namespace DB.Models;

public partial class Person
{
    public int Id { get; set; }

    public string? Names { get; set; }

    public string? LastNames { get; set; }

    public int? IdentificationNumber { get; set; }

    public string? Email { get; set; }

    public string? IdentificationType { get; set; }

    public DateTime? CreationDate { get; set; }

    public string? Identification { get; set; }

    public string? FullName { get; set; }
}
