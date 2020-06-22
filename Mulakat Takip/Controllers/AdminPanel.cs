using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mulakat_Takip.Database;
using Mulakat_Takip.Models;

namespace Mulakat_Takip.Controllers
{
    public class AdminPanel : Controller
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

        public AdminPanel(DatabaseContext context)
        {
            _context = context;
        }
        // GET: AdminPanel
        //int? id
        public async Task<IActionResult> Index()
        {
            var P_job = from e in _context.PanelOperations
                        select e;
            //GlobalVar.UserId = id.ToString();

            return View(P_job);
        }
        //public void OrnekPost(int? id)
        //{
        //    var P_panel = (from e in _context.PanelOperations
        //                where e.Panelid == id
        //                select e).ToList();

        //    P_panel[0].PanelStatus = "Onaylandı.";
        //    PanelOperations P_pan = P_panel[0];
        //    _context.Update(P_pan);
        //    _context.SaveChanges();
        //}

        // GET: AdminPanel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var panelOperations = await _context.PanelOperations
                .FirstOrDefaultAsync(m => m.Panelid == id);
            if (panelOperations == null)
            {
                return NotFound();
            }

            return View(panelOperations);
        }

        // GET: AdminPanel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminPanel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: AdminPanel/Edit/5
        public async Task<IActionResult> Confirmation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var panelOperations = await _context.PanelOperations.FindAsync(id);
            if (panelOperations == null)
            {
                return NotFound();
            }
            return View(panelOperations);
        }

        // POST: PanelOperations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirmation(int id, PanelOperations panelOperations)
        {
            if (id != panelOperations.Panelid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var P_panel = (from e in _context.PanelOperations
                                   where e.Panelid == id
                                   select e).ToList();

                    P_panel[0].PanelStatus = panelOperations.PanelStatus;
                    P_panel[0].PanelDefinition = panelOperations.PanelDefinition;
                    PanelOperations P_pan = P_panel[0];
                    _context.Update(P_pan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction(nameof(Index));
            }
            return View(panelOperations);
        }

        // GET: AdminPanel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var panelOperations = await _context.PanelOperations
                .FirstOrDefaultAsync(m => m.Panelid == id);
            if (panelOperations == null)
            {
                return NotFound();
            }

            return View(panelOperations);
        }

        // POST: PanelOperations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var panelOperations = await _context.PanelOperations.FindAsync(id);
            _context.PanelOperations.Remove(panelOperations);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
