using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

        public UserPanel(DatabaseContext context)
        {
            _context = context;
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
        public async Task<IActionResult> Create(PanelOperations panelOperations)
        {//
            //PanelOperations panelOperations
            if (ModelState.IsValid)
            {
                //using (var target = new MemoryStream())
                //{
                //    panelOperations.PanelFile.CopyTo(target);
                //    objfiles.DataFiles = target.ToArray();
                //}
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

        private bool PanelOperationsExists(int id)
        {
            return _context.PanelOperations.Any(e => e.Panelid == id);
        }
    }
}
