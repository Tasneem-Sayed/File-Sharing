using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FileSharing.Models
{
    public class InputViewModel
    {[Required]
        public IFormFile File { get; set; }
    }
    public class UploadViewModel
    {
        public string OriginalFileName { get; set; }
        public string FileName { get; set; }
        public decimal Size { get; set; }
        public string ContentType { get; set; }
        public DateTime UploadDate { get; set; }
        public string Id { get; set; }
    }
}
