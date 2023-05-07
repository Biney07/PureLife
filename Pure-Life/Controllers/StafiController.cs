using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pure_Life.Data;
using Pure_Life.Models;
using Pure_Life.Services;
using Pure_Life.ViewModel.Stafi;

namespace Pure_Life.Controllers
{
    public class StafiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ImageService _imageService;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly ICurrentUser _currentUser;
        private readonly UserManager<ApplicationUser> _userManager;

        public StafiController(ApplicationDbContext context, ImageService imageService, IMapper mapper, IAccountService accountService, ICurrentUser currentUser, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _imageService = imageService;
            _mapper = mapper;
            _accountService = accountService;
            _currentUser = currentUser;
            _userManager = userManager;
        }

        // GET: Stafi
        public async Task<IActionResult> Index()
        {
			var stafiList = await _context.Stafi
				.Include(s => s.Lemia)
				.Include(s => s.Nacionaliteti)
				.Include(s => s.Rolet)
				.Include(s => s.Shteti)
				.Where(s => s.IsDeleted == false).ToListAsync();

            return View(stafiList);
        }

        // GET: Stafi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
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
        public IActionResult Create()
        {
                ViewData["LemiaId"] = new SelectList(_context.Lemia.ToList(), "Id", "Emri");
                ViewData["NacionalitetiId"] = new SelectList(_context.Nacionaliteti.ToList(), "Id", "Emri");
                ViewData["RoletId"] = new SelectList(_context.Rolet.ToList(), "Id", "Emri");
            ViewData["ShtetiId"] = new SelectList(_context.Shteti.ToList(), "Id", "Emri");
     
            return View();
        }

        // POST: Stafi/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddStafiViewModel viewModel)
        {

            var stafi = _mapper.Map<Stafi>(viewModel);

            var stafiEmailatZyrtar = _context.Stafi.Where(s => s.EmailZyrtar == (viewModel.Emri.ToLower() + viewModel.Mbiemri.ToLower()).Replace(" ", "")+ "@purelife.net").Count();
            
            if (stafiEmailatZyrtar !=0)
            {
                var numriRandom = stafiEmailatZyrtar + 1;
                stafi.EmailZyrtar = (viewModel.Emri.ToLower() + viewModel.Mbiemri.ToLower()).Replace(" ", "") + (numriRandom == 0 ? "" : numriRandom) + "@purelife.net";
               

            }
            else
            {
              stafi.EmailZyrtar = (viewModel.Emri.ToLower() + viewModel.Mbiemri.ToLower()).Replace(" ", "") + "@purelife.net";
            }
            var user = _currentUser.GetCurrentUserName();
            if (viewModel.PictureUrl != null)
            {
                var result = await _imageService.AddPhotoAsync(viewModel.PictureUrl);
                stafi.PictureUrl = result.Url.ToString();
                stafi.PublicId = result.PublicId;
                stafi.InsertedFrom = user;
                stafi.InsertedDate= DateTime.Now;
                stafi.IsDeleted= false;

            }
            
            if (ModelState.IsValid)
            {
                await _accountService.RegistersStaf(stafi);
                _context.Add(stafi);
                await _context.SaveChangesAsync();
            }




            return RedirectToAction("Index");
        }
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Stafi == null)
			{
				return NotFound();
			}
			ViewData["LemiaId"] = new SelectList(_context.Lemia.ToList(), "Id", "Emri");
			ViewData["NacionalitetiId"] = new SelectList(_context.Nacionaliteti.ToList(), "Id", "Emri");
			ViewData["RoletId"] = new SelectList(_context.Rolet.ToList(), "Id", "Emri");
			ViewData["ShtetiId"] = new SelectList(_context.Shteti.ToList(), "Id", "Emri");

			var stafi = await _context.Stafi.FindAsync(id);
			/* stafiVM = new EditStafiViewModel
			{
				Id = stafi.Id,

				PublicId = stafi.PublicId,
				Imagelink = stafi.PictureUrl,
				NrLeternjoftimit = stafi.NrLeternjoftimit,
				Emri = stafi.Emri,
				Mbiemri = stafi.Mbiemri,
				Gjinia = stafi.Gjinia,
				DataLindjes = stafi.DataLindjes,
				NrLincences = stafi.NrLincences,
				NrTel = stafi.NrTel,
				Email = stafi.Email,
				EmailZyrtar = stafi.EmailZyrtar,
				RoletId = stafi.RoletId,
				ShtetiId = stafi.ShtetiId,
				Qyteti = stafi.Qyteti,
				NacionalitetiId = stafi.NacionalitetiId,
				LemiaId = stafi.LemiaId,
				Password = stafi.Password,
				ConfirmPassword = stafi.ConfirmPassword,
				InsertedFrom = stafi.InsertedFrom,
				InsertedDate = stafi.InsertedDate,
				ModifiedDate = (DateTime)stafi.ModifiedDate,
				ModifiedFrom = stafi.ModifiedFrom,
				IsDeleted = stafi.IsDeleted
			};*/

			if (stafi == null)
			{
				return NotFound();
			}
			return View(stafi);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, EditStafiViewModel stafiVM)
		{
			if (id != stafiVM.Id)
			{
				return NotFound();
			}

			var staff = await _context.Stafi.FindAsync(id);

			if (staff == null)
			{
				return NotFound();
			}

			var user = _currentUser.GetCurrentUserName();
			var stafiUpdated = new Stafi()
			{
				Id = staff.Id,
				PictureUrl = staff.PictureUrl ?? " " ,
				PublicId = staff.PublicId,
				NrLeternjoftimit = stafiVM.NrLeternjoftimit,
				Emri = staff.Emri,
				Mbiemri = stafiVM.Mbiemri,
				Gjinia = stafiVM.Gjinia,
				DataLindjes = stafiVM.DataLindjes,
				NrLincences = stafiVM.NrLincences,
				NrTel = stafiVM.NrTel,
				Email = staff.Email,
				EmailZyrtar = staff.EmailZyrtar,
				RoletId = stafiVM.RoletId,
				ShtetiId = stafiVM.ShtetiId,
				Qyteti = stafiVM.Qyteti,
				NacionalitetiId = stafiVM.NacionalitetiId,
				LemiaId = stafiVM.LemiaId,
				Password = staff.Password,
				ConfirmPassword = staff.ConfirmPassword,
				InsertedFrom = staff.InsertedFrom,
				InsertedDate = staff.InsertedDate,
				ModifiedDate = DateTime.Now,
				ModifiedFrom = user,
				IsDeleted = staff.IsDeleted,
			};
			staff.NrLeternjoftimit = stafiVM.NrLeternjoftimit;
			staff.Mbiemri = stafiVM.Mbiemri;
			staff.Gjinia = stafiVM.Gjinia;
			staff.DataLindjes = stafiVM.DataLindjes;
			staff.NrLincences = stafiVM.NrLincences;
			staff.NrTel = stafiVM.NrTel;
			staff.RoletId = stafiVM.RoletId;
			staff.ShtetiId = stafiVM.ShtetiId;
			staff.Qyteti = stafiVM.Qyteti;
			staff.NacionalitetiId = stafiVM.NacionalitetiId;
			staff.LemiaId = stafiVM.LemiaId;
			staff.ModifiedDate = DateTime.Now;
			staff.ModifiedFrom = user;

			if (stafiVM.PictureUrl != null)
			{
				var result = await _imageService.AddPhotoAsync(stafiVM.PictureUrl);

				if (staff.PublicId != null)
				{
					// delete the existing photo from Cloudinary
					await _imageService.DeletePhotoAsync(staff.PublicId);
				}

				staff.PictureUrl = result.Url.ToString();
				staff.PublicId = result.PublicId;
			}

			try
			{
				_context.Update(staff);
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!StafiExists(staff.Id))
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



		/*	[HttpPost]
			[ValidateAntiForgeryToken]



			public async Task<IActionResult> Edit(int id, EditStafiViewModel stafiVM)
			{
				if (id != stafiVM.Id)
				{
					return NotFound();
				}

				var staff = _context.Stafi.Where(x => x.Id == id).FirstOrDefault();

				// Detach the already tracked instance of Stafi
				_context.Entry(staff).State = EntityState.Detached;

				var user = _currentUser.GetCurrentUserName();
				var stafiUpdated = new Stafi()
				{
					Id = staff.Id,
					PictureUrl = staff.PictureUrl,
					PublicId = staff.PublicId,
					NrLeternjoftimit = stafiVM.NrLeternjoftimit,
					Emri = staff.Emri,
					Mbiemri = stafiVM.Mbiemri,
					Gjinia = stafiVM.Gjinia,
					DataLindjes = stafiVM.DataLindjes,
					NrLincences = stafiVM.NrLincences,
					NrTel = stafiVM.NrTel,
					Email = staff.Email,
					EmailZyrtar = staff.EmailZyrtar,
					RoletId = stafiVM.RoletId,
					ShtetiId = stafiVM.ShtetiId,
					Qyteti = stafiVM.Qyteti,
					NacionalitetiId = stafiVM.NacionalitetiId,
					LemiaId = stafiVM.LemiaId,
					Password = staff.Password,
					ConfirmPassword = staff.ConfirmPassword,
					InsertedFrom = staff.InsertedFrom,
					InsertedDate = staff.InsertedDate,
					ModifiedDate = DateTime.Now,
					ModifiedFrom = user,
					IsDeleted = staff.IsDeleted,
				};

				if (stafiVM.PictureUrl != null)
				{
					var result = await _imageService.AddPhotoAsync(stafiVM.PictureUrl);
					stafiUpdated.PictureUrl = result.Url.ToString();
					stafiUpdated.PublicId = result.PublicId;
				}

				try
				{
					_context.Update(stafiUpdated);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!StafiExists(stafiUpdated.Id))
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
	*/





		// GET: Stafi/Delete/5
		public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
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
            var stafi = await _context.Stafi.FindAsync(id);

            if (stafi == null)
            {
                return NotFound();
            }

            if (stafi.PublicId != null)
            {

                await _imageService.DeletePhotoAsync(stafi.PublicId);

            }

			stafi.IsDeleted= true;
            
			await _context.SaveChangesAsync();
			var user = await _userManager.FindByEmailAsync(stafi.Email);
			if (user == null)
			{
				ViewBag.ErrorMessage = $"User cannot be found";
				return View("NotFound");
			}
			else
			{
				var result = await _userManager.DeleteAsync(user);
				if (result.Succeeded)
				{
					return RedirectToAction(nameof(Index));
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}
		

            return RedirectToAction(nameof(Index));
        }
		private bool StafiExists(int id)
		{
			return _context.Stafi.Any(e => e.Id == id);
		}

	}
}