using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mulakat_Takip.Database;
using Mulakat_Takip.Models;

namespace Mulakat_Takip.Controllers
{
    public class UserPanel : Controller
    {
        public static class GlobalVar
        {
            static string _globalValue;
            public static string UserId
            {
                get
                {
                    return _globalValue;
                }
                set
                {
                    _globalValue = value;
                }
            }
        }
        private readonly DatabaseContext _context;
        private readonly IHostingEnvironment _environment;

        public UserPanel(DatabaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        // GET: PanelOperations
        public async Task<IActionResult> Index(int? id)
        {
            var P_job = from e in _context.PanelOperations
                        where e.UserId == id
                        select e;
            GlobalVar.UserId = id.ToString();
            //var G_job = await _context.PanelOperations.FindAsync(id);

            return View(P_job);
        }

        // GET: UserPanel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserPanel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PanelOperations panelOperations, IFormFile PanelFile)
        {//
            //PanelOperations panelOperations
            if (ModelState.IsValid)
            {
                //using (var target = new MemoryStream())
                //{
                //    panelOperations.PanelFile.CopyTo(target);
                //    objfiles.DataFiles = target.ToArray();
                //}
                //IFormFile ImageFile = panelOperations.Files;
                panelOperations.UserId = Convert.ToInt32(GlobalVar.UserId);
                var filename = ContentDispositionHeaderValue.Parse(PanelFile.ContentDisposition).FileName.Trim('"');
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", PanelFile.FileName);
                using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
                {
                    await PanelFile.CopyToAsync(stream);
                }
                panelOperations.PanelFile = filename;


                _context.Add(panelOperations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(panelOperations);
            //if (files != null)
            //{
            //    if (files.Length > 0)
            //    {
            //        //Getting FileName
            //        var fileName = Path.GetFileName(files.FileName);
            //        //Getting file Extension
            //        var fileExtension = Path.GetExtension(fileName);
            //        // concatenating  FileName + FileExtension
            //        var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

            //        var objfiles = new Files()
            //        {
            //            DocumentId = 0,
            //            Name = newFileName,
            //            FileType = fileExtension,
            //            CreatedOn = DateTime.Now
            //        };

            //        using (var target = new MemoryStream())
            //        {
            //            files.CopyTo(target);
            //            objfiles.DataFiles = target.ToArray();
            //        }
            //        var objfiles2 = new PanelOperations()
            //        {
            //            UserId = 5,
            //            PanelFile = objfiles.DataFiles
            //        };
            //        _context.PanelOperations.Add(objfiles2);
            //        _context.SaveChanges();

            //    }
        //}
        //    return View();
        }

        public async Task<IActionResult> Download(string? filename)
        {
            //if (filename == null)
            //    return Content("filename not present");
            filename = "Zafer UZUN önyazı.docx";

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot","Files", filename);
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        // GET: UserPanel/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserPanel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserPanel/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserPanel/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Deneme()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Deneme(IFormFile Getfile)
        {
            string fileName = Guid.NewGuid().ToString();
            if (Getfile != null)
            {
                var Upload = Path.Combine(_environment.WebRootPath, "Belgeler", fileName);
                Getfile.CopyTo(new FileStream(Upload, FileMode.Create));
            }
            return View();
        }
        private bool PanelOperationsExists(int id)
        {
            return _context.PanelOperations.Any(e => e.Panelid == id);
        }
    }
}
