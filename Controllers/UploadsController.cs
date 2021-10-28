using FileSharing.Data;
using FileSharing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FileSharing.Controllers
{[Authorize]
    public class UploadsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment env;

        //IWebHostEnvironment to access to images folder
        public UploadsController(ApplicationDbContext context,IWebHostEnvironment env)
        {
            this._db = context;
            this.env = env;
        }

        
        public IActionResult Index()
        {
            // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _db.Uploads.Where(u => u.UserId == UserId)
                //write uploadsviewmodel beacuse in index i use uploadsviewmodel 
                .Select(u => new UploadViewModel
                {
                    Id=u.Id,
                    FileName=u.FileName,
                    OriginalFileName= u.OriginalFileName,
                    ContentType=u.ContentType,
                    Size=u.Size,
                    UploadDate=u.UploadDate
                }) ;
           
            return View(result);
        }
        private string UserId
        {
            get { return User.FindFirstValue(ClaimTypes.NameIdentifier); }
        }

        [HttpPost]
        [AllowAnonymous]//يسمح لاى حد مجهول يدخل يعمل سيرش
        public IActionResult Results(String term)
        {
            var model = _db.Uploads.Where(u => u.OriginalFileName.Contains(term))
                .Select(u => new UploadViewModel
                { 
                    FileName = u.FileName,
                    OriginalFileName = u.OriginalFileName,
                    ContentType = u.ContentType,
                    Size = u.Size,
                    UploadDate = u.UploadDate
                });

            return View(model);

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Create(InputViewModel model)
        {
            if (ModelState.IsValid)
            { //To Avoid overwrite complix when folder uploaded with same extention
                //rer-grrg-gggee
                var newName = Guid.NewGuid().ToString();
                //To Get path of file
                var extension = Path.GetExtension(model.File.FileName);
                //newname+extension
                var fileName = string.Concat(newName , extension);
                //To access wwwroot
                var root = env.WebRootPath;
               var path = Path.Combine(root, "Uploads", fileName);
                using (var file=System.IO.File.Create(path))
                {
                   await model.File.CopyToAsync(file); //file stream

                }
                //save in database
                await _db.Uploads.AddAsync(new Uploads
                {OriginalFileName=model.File.FileName,
                    FileName = fileName,
                    ContentType = model.File.ContentType,
                    Size=model.File.Length,
                    UserId=UserId,

                }) ;
                
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete (string id)
        {
            var SelectUploads = await _db.Uploads.FindAsync(id);
            if(SelectUploads == null)
            {
                return NotFound();
            }
            if (SelectUploads.UserId != UserId)
            {
                return NotFound();
            }
            return View(SelectUploads);

        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmation(string id)
        {
            var SelectUploads = await _db.Uploads.FindAsync(id);
            if (SelectUploads == null)
            {
                return NotFound();
            }
            if (SelectUploads.UserId != UserId)
            {
                return NotFound();
            }
            _db.Uploads.Remove(SelectUploads);
           await _db.SaveChangesAsync();
            return RedirectToAction("Index");
           
        }
    }
}
