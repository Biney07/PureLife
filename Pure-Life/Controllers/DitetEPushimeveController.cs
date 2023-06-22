using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pure_Life.Data;
using Pure_Life.Models;
using Pure_Life.Services;
using Pure_Life.ViewModel.DitetEPushimeve;

namespace Pure_Life.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class DitetEPushimeveController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;
        public DitetEPushimeveController(ApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser= currentUser;
        }

        // GET: DitetEPushimeve
        /*        public async Task<IActionResult> Index()
                {
                      return _context.DitetEPushimeve != null ? 
                                  View(await _context.DitetEPushimeve.ToListAsync()) :
                                  Problem("Entity set 'ApplicationDbContext.DitetEPushimeve'  is null.");
                }*/

        public async Task<IActionResult> Index()
        {
            var pushimeList = await _context.DitetEPushimeve.ToListAsync();

            // Sort the list by DitaEPushimit, but put the items with a DitaEPushimit value
            // less than DateTime.Now at the end of the list
            pushimeList = pushimeList.OrderBy(p => DateTime.Compare(p.DitaEPushimit, DateTime.Now) < 0 ? int.MaxValue : DateTime.Compare(p.DitaEPushimit, DateTime.Now))
                                     .ToList();

            // Find the index of the first item with a DitaEPushimit value equal to DateTime.Now
            var currentDayIndex = pushimeList.FindIndex(p => DateTime.Compare(p.DitaEPushimit.Date, DateTime.Today) == 0);

            if (currentDayIndex > 0)
            {
                // Swap the positions of the first and current day items
                var temp = pushimeList[0];
                pushimeList[0] = pushimeList[currentDayIndex];
                pushimeList[currentDayIndex] = temp;
            }

            return View(pushimeList);
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
        public async Task<IActionResult> Create(AddPushimiViewModel ditetEPushimeve)
        {

            if (!ModelState.IsValid)
            {
                return View(ditetEPushimeve);
            }
            var user = _currentUser.GetCurrentUserName();
            var pushimi = new DitetEPushimeve()
            {
                Emri=ditetEPushimeve.Emri,
                Festa = ditetEPushimeve.Festa,
                DitaEPushimit = ditetEPushimeve.DitaEPushimit,
                InsertedDate = DateTime.Now,
                InsertedFrom = user
            };
            await _context.AddAsync(pushimi);   
            await _context.SaveChangesAsync();  
            return RedirectToAction("Index");
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Emri,Festa,DitaEPushimit")] EditPushimiViewModel ditetEPushimeve)
        {
            if (id != ditetEPushimeve.Id)
            {
                return NotFound();
            }
            var pushimi = await _context.DitetEPushimeve.FirstOrDefaultAsync(x=>x.Id== id);

            if (ModelState.IsValid  && pushimi != null)
            {
                try
                {
                    pushimi.Emri = ditetEPushimeve.Emri;
                    pushimi.Festa = ditetEPushimeve.Festa;
                    pushimi.DitaEPushimit = ditetEPushimeve.DitaEPushimit;
                    
                    _context.Update(pushimi);
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
