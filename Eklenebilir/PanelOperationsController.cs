using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mulakat_Takip.Database;
using Mulakat_Takip.Models;

namespace Mulakat_Takip.Controllers
{
    public class PanelOperationsController : Controller
    {
        private readonly DatabaseContext _context;

        public PanelOperationsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: PanelOperations
        public async Task<IActionResult> Index()
        {
            var G_jobs = _context.PanelOperations.Include(c => _context.Users);

            return View(await _context.PanelOperations.ToListAsync());
        }

        // GET: PanelOperations/Details/5
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

        // GET: PanelOperations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PanelOperations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Panelid,UserId,PanelFile,PanelName,PanelSurname,PanelDate")] PanelOperations panelOperations)
        {
            if (ModelState.IsValid)
            {
                _context.Add(panelOperations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(panelOperations);
        }

        // GET: PanelOperations/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
        public async Task<IActionResult> Edit(int id,PanelOperations panelOperations)
        {
            if (id != panelOperations.Panelid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(panelOperations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PanelOperationsExists(panelOperations.Panelid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(panelOperations);
        }

        // GET: PanelOperations/Delete/5
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

        private bool PanelOperationsExists(int id)
        {
            return _context.PanelOperations.Any(e => e.Panelid == id);
        }
    }
}
