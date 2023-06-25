using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pure_Life.Data;
using Pure_Life.Models;
using Pure_Life.Services;
using Pure_Life.ViewModel.Lemia;

namespace Pure_Life.Controllers
{
    public class LemiaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;
        public LemiaController(ApplicationDbContext context,ICurrentUser currentUser)
        {
            _context = context;
            _currentUser= currentUser;
        }

		// GET: Lemias
		/* public async Task<IActionResult> Index()
		 {
			   return _context.Lemia != null ? 
						   View(await _context.Lemia.ToListAsync()) :
						   Problem("Entity set 'ApplicationDbContext.Lemia'  is null.");
		 }*/

		public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
		{
			ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			ViewData["EmriSortParm"] = sortOrder == "Emri" ? "emri_desc" : "Emri";
			// Add other sort parameters for Lemia properties if needed

			ViewData["CurrentFilter"] = searchString;
			ViewData["CurrentSort"] = sortOrder;

			if (searchString != null)
			{
				pageNumber = 1;
			}
			else
			{
				searchString = currentFilter;
			}

			var lemiaList = from l in _context.Lemia
							select l;

			if (!String.IsNullOrEmpty(searchString))
			{
				lemiaList = lemiaList.Where(l => l.Emri.Contains(searchString));
			}

			switch (sortOrder)
			{
				case "name_desc":
					lemiaList = lemiaList.OrderByDescending(l => l.Emri);
					break;
				case "Emri":
					lemiaList = lemiaList.OrderBy(l => l.Emri);
					break;
				// Add cases for sorting Lemia properties if needed
				default:
					lemiaList = lemiaList.OrderBy(l => l.InsertedDate);
					break;
			}

			var pageSize = 10;
			var lemiaListWithSorting = await PaginatedList<Lemia>.CreateAsync(lemiaList.AsNoTracking(), pageNumber ?? 1, pageSize);

			return View(lemiaListWithSorting);
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
        public async Task<IActionResult> Create(AddLemiaViewModel lemiaVM)
        {
            var user = _currentUser.GetCurrentUserName();
      
            if (!ModelState.IsValid)
            {
                
                return View(lemiaVM);
            }
            var lemiaNew = new Lemia()
            {
                Emri = lemiaVM.Emri,
                InsertedDate = DateTime.Now,
                InsertedFrom = user,
            };
            _context.Add(lemiaNew);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Emri,ModifiedDate,ModifiedFrom")] EditLemiaViewModel lemiaVM)
        {
            if (id != lemiaVM.Id)
            {
                return NotFound();
            }
          var user = _currentUser.GetCurrentUserName(); 
            var lemiaZgjedhur =await _context.Lemia.Where(x=>x.Id == id).FirstOrDefaultAsync();

            if (ModelState.IsValid)
            {
                try
                {
                    lemiaZgjedhur.Id= lemiaVM.Id;
                    lemiaZgjedhur.Emri=lemiaVM.Emri;
                    lemiaZgjedhur.ModifiedDate = DateTime.Now;
                    lemiaZgjedhur.ModifiedFrom= user;
                    _context.Update(lemiaZgjedhur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LemiaExists(lemiaVM.Id))
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
            return View(lemiaVM);
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
            var stafi = await _context.Stafi.Where(s=>s.LemiaId== id).FirstOrDefaultAsync();
            var lemia = await _context.Lemia.FindAsync(id);
            if (lemia != null)
            {
           /*     _context.Stafi.RemoveRange(stafi);*/
                _context.Lemia.Remove(lemia);
               
            }
            
            await _context.SaveChangesAsync();
           /* await _context.SaveChangesAsync();*/
            return RedirectToAction(nameof(Index));
        }

        private bool LemiaExists(int id)
        {
          return (_context.Lemia?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
