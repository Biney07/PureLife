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
using Pure_Life.ViewModel.Sherbimet;

namespace Pure_Life.Controllers
{
	public class SherbimetController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly ICurrentUser _currentUser;
		public SherbimetController(ApplicationDbContext context, ICurrentUser currentUser)
		{
			_context = context;
			_currentUser = currentUser;
		}

		// GET: Sherbimet
		public async Task<IActionResult> Index()
		{
			return View(await _context.Sherbimet.Where(x => x.IsDeleted == false).ToListAsync()); 
		}

			/*return _context.Sherbimet != null ?
						View(await _context.Sherbimet.Where(x => x.IsDeleted == false).ToListAsync()) :
						Problem("Entity set 'ApplicationDbContext.Sherbimet'  is null.");*/
		

		// GET: Sherbimet/Details/5
		public async Task<IActionResult> Details(int id)
		{
			if (id == null || _context.Sherbimet == null)
			{
				return NotFound();
			}

			var sherbimet = await _context.Sherbimet.Where(m => m.Id == id).FirstOrDefaultAsync();
			if (sherbimet == null)
			{
				return NotFound();
			}

			return View(sherbimet);
		}

		// GET: Sherbimet/Create
		public IActionResult Create()
		{
			ViewData["LemiaId"] = new SelectList(_context.Lemia.ToList(), "Id", "Emri");
			return View();
		}

		// POST: Sherbimet/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(AddSherbimetViewModel sherbimet)
		{
			if (!ModelState.IsValid)
			{
				return View(sherbimet);
			}
			var user = _currentUser.GetCurrentUserName();
			var sherbimi = new Sherbimet()
			{
				Emri = sherbimet.Emri,
				Pershkrimi = sherbimet.Pershkrimi,
				Cmimi = sherbimet.Cmimi,
				LemiaId = sherbimet.LemiaId,
				InsertedFrom = user,
				InsertedDate = DateTime.Now

			};
			await _context.Sherbimet.AddAsync(sherbimi);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		// GET: Sherbimet/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Sherbimet == null)
			{
				return NotFound();
			}
			ViewData["LemiaId"] = new SelectList(_context.Lemia.ToList(), "Id", "Emri");
			var sherbimet = await _context.Sherbimet.FindAsync(id);
			if (sherbimet == null)
			{
				return NotFound();
			}
			return View(sherbimet);
		}

		// POST: Sherbimet/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Emri,Pershkrimi,Cmimi,LemiaId")] EditSherbimetViewModel sherbimet)
		{
			if (id != sherbimet.Id)
			{
				return NotFound();
			}
			var user = _currentUser.GetCurrentUserName();
			var sherbimi = await _context.Sherbimet.Where(x => x.Id == sherbimet.Id).FirstOrDefaultAsync();
			if (ModelState.IsValid)
			{
				try
				{
					sherbimi.Id = id;
					sherbimi.Emri = sherbimet.Emri;
					sherbimi.Pershkrimi = sherbimet.Pershkrimi;
					sherbimi.Cmimi = sherbimet.Cmimi;
					sherbimi.LemiaId = sherbimet.LemiaId;
					sherbimi.ModifiedDate = DateTime.Now;
					sherbimi.ModifiedFrom = user;
					_context.Update(sherbimi);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!SherbimetExists(sherbimet.Id))
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
			return View(sherbimet);
		}

		// GET: Sherbimet/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Sherbimet == null)
			{
				return NotFound();
			}

			var sherbimet = await _context.Sherbimet
				.FirstOrDefaultAsync(m => m.Id == id);
			if (sherbimet == null)
			{
				return NotFound();
			}

			return View(sherbimet);
		}

		// POST: Sherbimet/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Sherbimet == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Sherbimet'  is null.");
			}
			var sherbimet = await _context.Sherbimet.FindAsync(id);
			if (sherbimet != null)
			{
				_context.Sherbimet.Remove(sherbimet);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool SherbimetExists(int id)
		{
			return (_context.Sherbimet?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
