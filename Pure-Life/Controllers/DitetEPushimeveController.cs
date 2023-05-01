using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pure_Life.Data;
using Pure_Life.Models;

namespace Pure_Life.Controllers
{
    public class DitetEPushimeveController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DitetEPushimeveController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DitetEPushimeve
        public async Task<IActionResult> Index()
        {
              return _context.DitetEPushimeve != null ? 
                          View(await _context.DitetEPushimeve.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.DitetEPushimeve'  is null.");
        }

        // GET: DitetEPushimeve/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DitetEPushimeve == null)
            {
                return NotFound();
            }

            var ditetEPushimeve = await _context.DitetEPushimeve
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ditetEPushimeve == null)
            {
                return NotFound();
            }

            return View(ditetEPushimeve);
        }

        // GET: DitetEPushimeve/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DitetEPushimeve/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Festa,DitaEPushimit,InsertedFrom,InsertedDate")] DitetEPushimeve ditetEPushimeve)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ditetEPushimeve);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ditetEPushimeve);
        }

        // GET: DitetEPushimeve/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DitetEPushimeve == null)
            {
                return NotFound();
            }

            var ditetEPushimeve = await _context.DitetEPushimeve.FindAsync(id);
            if (ditetEPushimeve == null)
            {
                return NotFound();
            }
            return View(ditetEPushimeve);
        }

        // POST: DitetEPushimeve/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Festa,DitaEPushimit,InsertedFrom,InsertedDate")] DitetEPushimeve ditetEPushimeve)
        {
            if (id != ditetEPushimeve.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ditetEPushimeve);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DitetEPushimeveExists(ditetEPushimeve.Id))
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
            return View(ditetEPushimeve);
        }

        // GET: DitetEPushimeve/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DitetEPushimeve == null)
            {
                return NotFound();
            }

            var ditetEPushimeve = await _context.DitetEPushimeve
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ditetEPushimeve == null)
            {
                return NotFound();
            }

            return View(ditetEPushimeve);
        }

        // POST: DitetEPushimeve/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DitetEPushimeve == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DitetEPushimeve'  is null.");
            }
            var ditetEPushimeve = await _context.DitetEPushimeve.FindAsync(id);
            if (ditetEPushimeve != null)
            {
                _context.DitetEPushimeve.Remove(ditetEPushimeve);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DitetEPushimeveExists(int id)
        {
          return (_context.DitetEPushimeve?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
