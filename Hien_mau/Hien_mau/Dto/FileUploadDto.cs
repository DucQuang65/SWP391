﻿using Microsoft.AspNetCore.Mvc;

namespace Hien_mau.Dto
{
    public class FileUploadDto
    {
        [FromForm(Name = "file")]
        public IFormFile File { get; set; }
    }
}
