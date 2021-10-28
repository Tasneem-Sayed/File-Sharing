using FileSharing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileSharing.Services
{
    interface IUploadservices
    {
        IQueryable<UploadViewModel> GetAll();
        IQueryable<UploadViewModel> Search(string term);
    }
}
