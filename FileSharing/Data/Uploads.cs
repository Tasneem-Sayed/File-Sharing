using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileSharing.Data
{
    public class Uploads
    {
        public Uploads()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public decimal Size { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
