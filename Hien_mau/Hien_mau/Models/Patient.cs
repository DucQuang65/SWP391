﻿using System;
using System.Collections.Generic;

namespace Hien_mau.Models;

public partial class Patient
{
    public int PatientId { get; set; }

    public string? FullName { get; set; }

    public string? Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public int? Age { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }
}
