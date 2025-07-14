using System;
using System.Collections.Generic;

namespace Hien_mau.Models;

public partial class UploadedFile
{
    public int Id { get; set; }

    public string? FileName { get; set; }

    public string? FileUrl { get; set; }

    public DateTime? UploadDate { get; set; }

    public int? UploadedBy { get; set; }
}
