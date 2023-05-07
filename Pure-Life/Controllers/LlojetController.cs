using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pure_Life.Data;
using Pure_Life.Models;
using Pure_Life.Models.Analiza;
using Pure_Life.Services;
using Pure_Life.ViewModel.Lemia;
using Pure_Life.ViewModel.Llojet;

namespace Pure_Life.Controllers
{
    public class LlojetController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;
        public LlojetController(ApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }
        public async Task<ActionResult> Index()
        {
            return View(await _context.Llojet.ToListAsync());
        }

        // GET: Llojet/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Lloji lloji = await _context.Llojet.FindAsync(id);
            if (lloji == null)
            {
                return NotFound();
            }
            return View(lloji);
        }

        // GET: Llojet/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Llojet/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Emri,VleratReferente")] AddLlojiViewModel llojiVM)
        {
		

			if (!ModelState.IsValid)
            {

				return View(llojiVM);
			}
            var llojiNew = new Lloji()
            {
                Emri = llojiVM.Emri,
                VleratReferente = llojiVM.VleratReferente

			};
			_context.Add(llojiNew);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}

        // GET: Llojet/Edit/5
        public async Task<IActionResult> Edit(int? id)

        {
            if (id == null)
            {
                return BadRequest();
            }
            Lloji lloji = await _context.Llojet.FindAsync(id);
            if (lloji == null)
            {
                return NotFound();
            }

            var updateLlojiVM = new UpdateLlojiViewModel()
            {
                Id = lloji.Id,
                Emri = lloji.Emri,
                VleratReferente = lloji.VleratReferente
            };

            return View(updateLlojiVM);
        }


        // POST: Llojet/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateLlojiViewModel updateLlojiVM)
        {
            if (ModelState.IsValid)
            {
                Lloji lloji = await _context.Llojet.FindAsync(updateLlojiVM.Id);
                if (lloji != null)
                {
                    lloji.Emri = updateLlojiVM.Emri;
                    lloji.VleratReferente = updateLlojiVM.VleratReferente;

                    _context.Entry(lloji).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
            return View(updateLlojiVM);
        }


        // GET: Llojet/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Lloji lloji = await _context.Llojet.FindAsync(id);
            if (lloji == null)
            {
                return NotFound();
            }
            return View(lloji);
        }

        // POST: Llojet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Lloji lloji = await _context.Llojet.FindAsync(id);
            _context.Llojet.Remove(lloji);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
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