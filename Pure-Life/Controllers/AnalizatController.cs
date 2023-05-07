using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pure_Life.Data;
using Pure_Life.Models.Analiza;
using Pure_Life.Services;
using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pure_Life.ViewModel.Analizat;

namespace Pure_Life.Controllers
{
    public class AnalizatController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;
        public AnalizatController(ApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }
        public async Task<ActionResult> Index()
        {
            return View(await _context.Analizat.ToListAsync());
        }

        // GET: Analizat/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analiza = await _context.Analizat
                .FirstOrDefaultAsync(a => a.Id == id);

            if (analiza == null)
            {
                return NotFound();
            }

            var analizaLlojet = await _context.AnalizatLlojet
                .Include(al => al.Lloji)
                .Where(al => al.AnalizaId == id)
                .ToListAsync();

            var viewModel = new AnalizaDetailsViewModel
            {
                Analiza = analiza,
                Llojet = analizaLlojet.Select(al => al.Lloji).ToList()
            };

            return View(viewModel);
        }


        // GET: Analiza/Create
        public async Task<IActionResult> Create()
        {
            var llojet = await _context.Llojet.ToListAsync();
            var viewModel = new AnalizaCreateViewModel
            {
                Llojet = llojet,
                SelectedLlojet = new List<int>()
            };

            return View(viewModel);
        }

        // POST: Analiza/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnalizaCreateViewModel analizaVm)
        {
            if (!ModelState.IsValid)
            {
                var analiza = new Analiza
                {
                    Emri = analizaVm.Emri,
                    Cmimi = analizaVm.Cmimi,
                    Data = analizaVm.Data
                };

                _context.Analizat.Add(analiza);
                await _context.SaveChangesAsync();

                foreach (var llojiId in analizaVm.SelectedLlojet)
                {
                    var analizaLloji = new AnalizaLloji
                    {
                        AnalizaId = analiza.Id,
                        LlojiId = llojiId
                    };

                    _context.AnalizatLlojet.Add(analizaLloji);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            var llojet = await _context.Llojet.ToListAsync();
            analizaVm.Llojet = llojet;

            return View(analizaVm);
        }
        // GET: Analiza/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analiza = await _context.Analizat
                .Include(a => a.AnalizaLlojet)
                    .ThenInclude(al => al.Lloji)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (analiza == null)
            {
                return NotFound();
            }

            var viewModel = new AnalizaUpdateViewModel
            {
                Id = analiza.Id,
                Emri = analiza.Emri,
                Cmimi = analiza.Cmimi,
                Data = analiza.Data,
                Llojet = await _context.Llojet.ToListAsync(),
                SelectedLlojet = analiza.AnalizaLlojet.Select(al => al.LlojiId).ToList()
            };

            return View(viewModel);
        }

        // POST: Analiza/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AnalizaUpdateViewModel analizaVm)
        {
            if (id != analizaVm.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var analiza = await _context.Analizat
                    .Include(a => a.AnalizaLlojet)
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (analiza == null)
                {
                    return NotFound();
                }

                analiza.Emri = analizaVm.Emri;
                analiza.Cmimi = analizaVm.Cmimi;
                analiza.Data = analizaVm.Data;

                var selectedLlojet = analizaVm.SelectedLlojet;
                var existingLlojetIds = analiza.AnalizaLlojet.Select(al => al.LlojiId).ToList();

                // Add new Llojet
                var newLlojetIds = selectedLlojet.Except(existingLlojetIds).ToList();
                foreach (var llojiId in newLlojetIds)
                {
                    var analizaLloji = new AnalizaLloji
                    {
                        AnalizaId = id,
                        LlojiId = llojiId
                    };
                    _context.AnalizatLlojet.Add(analizaLloji);
                }

                // Remove deleted Llojet
                var deletedLlojetIds = existingLlojetIds.Except(selectedLlojet).ToList();
                foreach (var llojiId in deletedLlojetIds)
                {
                    var analizaLloji = analiza.AnalizaLlojet.FirstOrDefault(al => al.LlojiId == llojiId);
                    if (analizaLloji != null)
                    {
                        _context.AnalizatLlojet.Remove(analizaLloji);
                    }
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            analizaVm.Llojet = await _context.Llojet.ToListAsync();
            return View(analizaVm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var record = await _context.Analizat.FindAsync(id);

            if (record == null)
            {
                return NotFound();
            }

            _context.Analizat.Remove(record);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}


