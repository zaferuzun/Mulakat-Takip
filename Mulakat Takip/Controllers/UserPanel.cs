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
            var P_Panel = from e in _context.PanelOperations
                        where e.UserId == id
                        select e;
            GlobalVar.UserId = id.ToString();
            //var G_job = await _context.PanelOperations.FindAsync(id);

            return View(P_Panel);
        }

        // GET: UserPanel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserPanel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PanelOperations G_panelOperations, IFormFile G_PanelFile)
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
                G_panelOperations.UserId = Convert.ToInt32(GlobalVar.UserId);
                var P_filename = ContentDispositionHeaderValue.Parse(G_PanelFile.ContentDisposition).FileName.Trim('"');
                var P_path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", G_PanelFile.FileName);
                using (System.IO.Stream stream = new FileStream(P_path, FileMode.Create))
                {
                    await G_PanelFile.CopyToAsync(stream);
                }
                G_panelOperations.PanelFile = P_filename;


                _context.Add(G_panelOperations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(G_panelOperations);
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
        [HttpPost]
        public async Task<IActionResult> Download(int? G_panelId)
        {
            string P_fileName = (from e in _context.PanelOperations
                                where e.Panelid == G_panelId
                                select  e.PanelFile).ToString();

            var P_path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot","Files", P_fileName);
            var P_memory = new MemoryStream();
            using (var stream = new FileStream(P_path, FileMode.Open))
            {
                await stream.CopyToAsync(P_memory);
            }
            P_memory.Position = 0;
            return File(P_memory, GetContentType(P_path), Path.GetFileName(P_path));
        }
        private string GetContentType(string G_path)
        {
            var P_types = GetMimeTypes();
            var P_ext = Path.GetExtension(G_path).ToLowerInvariant();
            return P_types[P_ext];
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

        //public IActionResult Deneme()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Deneme(IFormFile Getfile)
        //{
        //    string fileName = Guid.NewGuid().ToString();
        //    if (Getfile != null)
        //    {
        //        var Upload = Path.Combine(_environment.WebRootPath, "Belgeler", fileName);
        //        Getfile.CopyTo(new FileStream(Upload, FileMode.Create));
        //    }
        //    return View();
        }
        private bool PanelOperationsExists(int id)
        {
            return _context.PanelOperations.Any(e => e.Panelid == id);
        }
    }
}
