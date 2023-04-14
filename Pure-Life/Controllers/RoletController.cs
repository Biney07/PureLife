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
    public class RoletController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoletController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rolet
        public async Task<IActionResult> Index()
        {
              return _context.Rolet != null ? 
                          View(await _context.Rolet.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Rolet'  is null.");
        }

        // GET: Rolet/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rolet == null)
            {
                return NotFound();
            }

            var rolet = await _context.Rolet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rolet == null)
            {
                return NotFound();
            }

            return View(rolet);
        }

        // GET: Rolet/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rolet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Emri")] Rolet rolet)
        {
            ModelState.Remove("Stafi");
            if (ModelState.IsValid)
            {
                _context.Add(rolet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rolet);
        }

        // GET: Rolet/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rolet == null)
            {
                return NotFound();
            }

            var rolet = await _context.Rolet.FindAsync(id);
            if (rolet == null)
            {
                return NotFound();
            }
            return View(rolet);
        }

        // POST: Rolet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Emri")] Rolet rolet)
        {
            if (id != rolet.Id)
            {
                return NotFound();
            }
            ModelState.Remove("Stafi");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rolet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoletExists(rolet.Id))
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
            return View(rolet);
        }

        // GET: Rolet/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rolet == null)
            {
                return NotFound();
            }

            var rolet = await _context.Rolet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rolet == null)
            {
                return NotFound();
            }

            return View(rolet);
        }

        // POST: Rolet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rolet == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Rolet'  is null.");
            }
            var rolet = await _context.Rolet.FindAsync(id);
            if (rolet != null)
            {
                _context.Rolet.Remove(rolet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoletExists(int id)
        {
          return (_context.Rolet?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
