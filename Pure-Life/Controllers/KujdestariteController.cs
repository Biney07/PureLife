using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Pure_Life.Data;
using Pure_Life.Models;
using Pure_Life.Services;
using Pure_Life.ViewModel.Kujdestarite;

namespace Pure_Life.Controllers
{
    public class KujdestariteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;
        public KujdestariteController(ApplicationDbContext context,ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

		// GET: Kujdestarite
		/*   public async Task<IActionResult> Index()
		   {
			   var kujdestarite = await _context.Kujdestarite
								   .Include(k => k.Stafi)
								   .Where(k => k.IsDeleted==false || k.Data>DateTime.Now)
								   .OrderBy(k=>k.Data>=DateTime.Now)
								   .ToListAsync();
			   return View(kujdestarite);
		   }*/
		public async Task<IActionResult> Index()
		{
			var now = DateTime.Now;

			var kujdestarite = await _context.Kujdestarite
				.Include(k => k.Stafi)
				.Where(k => !k.IsDeleted && k.Data >= now.Date)
				.OrderBy(k => k.Data == now.Date ? 0 : (k.Data > now.Date ? 1 : 2))
				.ThenBy(k => k.Data)
				.ToListAsync();

			return View(kujdestarite);
		}


		// GET: Kujdestarite/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Kujdestarite == null)
            {
                return NotFound();
            }

            var kujdestarite = await _context.Kujdestarite
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kujdestarite == null)
            {
                return NotFound();
            }

            return View(kujdestarite);
        }

        // GET: Kujdestarite/Create
        public IActionResult Create()
        {
			ViewData["StafiId"] = new SelectList(_context.Stafi.ToList(), "Id", "EmailZyrtar");

			return View();
        }

        // POST: Kujdestarite/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddKujdestariViewModel kujdestariteVM)
        {
            if (!ModelState.IsValid)
            {
                return View(kujdestariteVM);
            }
            var stafi = await _context.Stafi.FirstOrDefaultAsync(x => x.Id == kujdestariteVM.StafiId);
            var lemia = await _context.Lemia.Where(x => x.Id == stafi.LemiaId).FirstOrDefaultAsync();
            var user = _currentUser.GetCurrentUserName();
            var kujdestaria = new Kujdestarite()
            {
                Data= kujdestariteVM.Data,
                Reparti=lemia.Emri,
                InsertedFrom=user,
                InsertedDate=DateTime.Now,
                StafiId = kujdestariteVM.StafiId,
            };
			switch (lemia.Emri)
			{
				case "Onkologji":
				case "Pulmologji":
				case "Dermatologji":
				case "Endokrinologji":
				case "Gastroenterologji":
				case "Gjinekologji":
				case "Hematologji":
				case "Infektive":
				case "Kardiologji":
				case "Neurologji":
				case "Reumatologji":
					kujdestaria.Kati = 1;
					break;
				case "Kardiokirurgji":
				case "Neurokirurgji":
				case "Ortopedi":
				case "Urologji":
					kujdestaria.Kati = 2;
					break;
				case "Oftalmologji":
				case "Pediatri":
				case "Psikiatri":
				case "Stomatologji":
				case "Radiologji":
				case "Biokimi":
					kujdestaria.Kati = 3;
					break;
			}


			await _context.Kujdestarite.AddAsync(kujdestaria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Kujdestarite/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
			ViewData["StafiId"] = new SelectList(_context.Stafi.ToList(), "Id", "EmailZyrtar");

			if (id == null || _context.Kujdestarite == null)
            {
                return NotFound();
            }

            var kujdestarite = await _context.Kujdestarite.FindAsync(id);
            if (kujdestarite == null)
            {
                return NotFound();
            }
            return View(kujdestarite);
        }

        // POST: Kujdestarite/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Data,StafiId")] EditKujdestariViewModel kujdestariteVM)
        {
            if (id != kujdestariteVM.Id)
            {
                return NotFound();
            }
            var user = _currentUser.GetCurrentUserName();
            var mjekuKujdestar = await _context.Kujdestarite.FirstOrDefaultAsync(x => x.Id == kujdestariteVM.Id);
            if (ModelState.IsValid)
            {
                mjekuKujdestar.StafiId = kujdestariteVM.StafiId;
                mjekuKujdestar.Data= kujdestariteVM.Data;
                mjekuKujdestar.ModifiedDate= DateTime.Now;
                mjekuKujdestar.ModifiedFrom = user;
                try
                {
                    _context.Update(mjekuKujdestar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KujdestariteExists(kujdestariteVM.Id))
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
            return View(kujdestariteVM);
        }

        // GET: Kujdestarite/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Kujdestarite == null)
            {
                return NotFound();
            }

            var kujdestarite = await _context.Kujdestarite
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kujdestarite == null)
            {
                return NotFound();
            }

            return View(kujdestarite);
        }

        // POST: Kujdestarite/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kujdestarite == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Kujdestarite'  is null.");
            }
            var kujdestarite = await _context.Kujdestarite.FindAsync(id);
            if (kujdestarite != null)
            {
                _context.Kujdestarite.Remove(kujdestarite);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KujdestariteExists(int id)
        {
          return (_context.Kujdestarite?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
