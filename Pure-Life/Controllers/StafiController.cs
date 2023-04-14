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
    public class StafiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StafiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Stafi
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Stafi.Include(s => s.Lemia).Include(s => s.Nacionaliteti).Include(s => s.Rolet).Include(s => s.Shteti);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Stafi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Stafi == null)
            {
                return NotFound();
            }

            var stafi = await _context.Stafi
                .Include(s => s.Lemia)
                .Include(s => s.Nacionaliteti)
                .Include(s => s.Rolet)
                .Include(s => s.Shteti)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stafi == null)
            {
                return NotFound();
            }

            return View(stafi);
        }

        // GET: Stafi/Create
        //
        public IActionResult Create()
        {
            ViewData["LemiaId"] = new SelectList(_context.Lemia, "Id", "Emri");
            ViewData["NacionalitetiId"] = new SelectList(_context.Nacionaliteti, "Id", "Emri");
            ViewData["RoletId"] = new SelectList(_context.Rolet, "Id", "Emri");
            ViewData["ShtetiId"] = new SelectList(_context.Shteti, "Id", "Emri");
            return View();
        }

        // POST: Stafi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NrLeternjoftimit,Emri,Mbiemri,Gjinia,DataLindjes,NrLincences,NrTel,RoletId,ShtetiId,Qyteti,NacionalitetiId,LemiaId,Email,Password,InsertedFrom,InsertedDate,ModifiedDate,ModifiedFrom")] Stafi stafi)
        {
            ModelState.Remove("Lemia");
            ModelState.Remove("Shteti");
            ModelState.Remove("Nacionaliteti");
            ModelState.Remove("Rolet");
            if (ModelState.IsValid)
            {
                _context.Add(stafi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LemiaId"] = new SelectList(_context.Lemia, "Id", "Id", stafi.Lemia.Emri);
            ViewData["NacionalitetiId"] = new SelectList(_context.Nacionaliteti, "Id", "Id", stafi.Nacionaliteti.Emri);
            ViewData["RoletId"] = new SelectList(_context.Rolet, "Id", "Id", stafi.Rolet.Emri);
            ViewData["ShtetiId"] = new SelectList(_context.Shteti, "Id", "Id", stafi.Shteti.Emri);
            return View(stafi);
        }

        // GET: Stafi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Stafi == null)
            {
                return NotFound();
            }

            var stafi = await _context.Stafi.FindAsync(id);
            if (stafi == null)
            {
                return NotFound();
            }
            ViewData["LemiaId"] = new SelectList(_context.Lemia, "Id", "Id", stafi.LemiaId);
            ViewData["NacionalitetiId"] = new SelectList(_context.Nacionaliteti, "Id", "Id", stafi.NacionalitetiId);
            ViewData["RoletId"] = new SelectList(_context.Rolet, "Id", "Id", stafi.RoletId);
            ViewData["ShtetiId"] = new SelectList(_context.Shteti, "Id", "Id", stafi.ShtetiId);
            return View(stafi);
        }

        // POST: Stafi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NrLeternjoftimit,Emri,Mbiemri,Gjinia,DataLindjes,NrLincences,NrTel,RoletId,ShtetiId,Qyteti,NacionalitetiId,LemiaId,Email,Password,InsertedFrom,InsertedDate,ModifiedDate,ModifiedFrom")] Stafi stafi)
        {
            if (id != stafi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stafi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StafiExists(stafi.Id))
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
            ViewData["LemiaId"] = new SelectList(_context.Lemia, "Id", "Id", stafi.LemiaId);
            ViewData["NacionalitetiId"] = new SelectList(_context.Nacionaliteti, "Id", "Id", stafi.NacionalitetiId);
            ViewData["RoletId"] = new SelectList(_context.Rolet, "Id", "Id", stafi.RoletId);
            ViewData["ShtetiId"] = new SelectList(_context.Shteti, "Id", "Id", stafi.ShtetiId);
            return View(stafi);
        }

        // GET: Stafi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Stafi == null)
            {
                return NotFound();
            }

            var stafi = await _context.Stafi
                .Include(s => s.Lemia)
                .Include(s => s.Nacionaliteti)
                .Include(s => s.Rolet)
                .Include(s => s.Shteti)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stafi == null)
            {
                return NotFound();
            }

            return View(stafi);
        }

        // POST: Stafi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Stafi == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Stafi'  is null.");
            }
            var stafi = await _context.Stafi.FindAsync(id);
            if (stafi != null)
            {
                _context.Stafi.Remove(stafi);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StafiExists(int id)
        {
          return (_context.Stafi?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
