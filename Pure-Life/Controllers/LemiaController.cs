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
    public class LemiaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LemiaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lemias
        public async Task<IActionResult> Index()
        {
              return _context.Lemia != null ? 
                          View(await _context.Lemia.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Lemia'  is null.");
        }

        // GET: Lemias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lemia == null)
            {
                return NotFound();
            }

            var lemia = await _context.Lemia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lemia == null)
            {
                return NotFound();
            }

            return View(lemia);
        }

        // GET: Lemias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lemias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Emri,InsertedFrom,InsertedDate,ModifiedDate,ModifiedFrom,IsDeleted")] Lemia lemia)
        {
            ModelState.Remove("Stafi");
            if (ModelState.IsValid)
            {
                _context.Add(lemia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lemia);
        }

        // GET: Lemias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lemia == null)
            {
                return NotFound();
            }

            var lemia = await _context.Lemia.FindAsync(id);
            if (lemia == null)
            {
                return NotFound();
            }
            return View(lemia);
        }

        // POST: Lemias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Emri,InsertedFrom,InsertedDate,ModifiedDate,ModifiedFrom,IsDeleted")] Lemia lemia)
        {
            if (id != lemia.Id)
            {
                return NotFound();
            }
            ModelState.Remove("Stafi");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lemia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LemiaExists(lemia.Id))
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
            return View(lemia);
        }

        // GET: Lemias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lemia == null)
            {
                return NotFound();
            }

            var lemia = await _context.Lemia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lemia == null)
            {
                return NotFound();
            }

            return View(lemia);
        }

        // POST: Lemias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lemia == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Lemia'  is null.");
            }
            var lemia = await _context.Lemia.FindAsync(id);
            if (lemia != null)
            {
                _context.Lemia.Remove(lemia);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LemiaExists(int id)
        {
          return (_context.Lemia?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
