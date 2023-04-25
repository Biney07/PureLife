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
                .ToListAsync();

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
            /*var viewModel = new StafiViewModel
            {*/
                /*  LemiaOptions = new SelectList(_context.Lemia.ToList(), "Id", "Emri"),
				  NacionalitetiOptions = new SelectList(_context.Nacionaliteti.ToList(), "Id", "Emri"),
				  RoletOptions = new SelectList(_context.Rolet.ToList(), "Id", "Emri"),
				  ShtetiOptions = new SelectList(_context.Shteti.ToList(), "Id", "Emri"),*/
                ViewData["LemiaId"] = new SelectList(_context.Lemia.ToList(), "Id", "Emri");
                ViewData["NacionalitetiId"] = new SelectList(_context.Nacionaliteti.ToList(), "Id", "Emri");
                ViewData["RoletId"] = new SelectList(_context.Rolet.ToList(), "Id", "Emri");
            ViewData["ShtetiId"] = new SelectList(_context.Shteti.ToList(), "Id", "Emri");
            /*	};*/
            /*
                        return View(viewModel);*/
            return View();
        }

        // POST: Stafi/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddStafiViewModel viewModel)
        {

            var stafi = _mapper.Map<Stafi>(viewModel);
            stafi.EmailZyrtar = viewModel.Emri.ToLower() + viewModel.Mbiemri.ToLower() + "@purelife.net";

            var user = _currentUser.GetCurrentUserName();
            if (viewModel.PictureUrl != null)
            {
                var result = await _imageService.AddPhotoAsync(viewModel.PictureUrl);
                stafi.PictureUrl = result.Url.ToString();
                stafi.PublicId = result.PublicId;
                stafi.InsertedFrom = user;
                stafi.InsertedDate= DateTime.Now;
                stafi.ModifiedDate = null;
                stafi.ModifiedFrom = null;

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
            if(id==null || _context.Stafi==null)
            {
                return NotFound();
            }
			ViewData["LemiaId"] = new SelectList(_context.Lemia.ToList(), "Id", "Emri");
			ViewData["NacionalitetiId"] = new SelectList(_context.Nacionaliteti.ToList(), "Id", "Emri");
			ViewData["RoletId"] = new SelectList(_context.Rolet.ToList(), "Id", "Emri");
			ViewData["ShtetiId"] = new SelectList(_context.Shteti.ToList(), "Id", "Emri");

            var stafi = await _context.Stafi.FindAsync(id);
           
            if (stafi==null)
            {
                return NotFound();
            }
             return View(stafi);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		/*     public async Task<IActionResult> Edit(int id, [Bind("Id,NrLeternjoftimit,Emri,Mbiemri,Gjinia,DataLindjes,NrLincences,NrTel,PictureUrl,PublicId,RoletId,ShtetiId,Qyteti,NacionalitetiId,LemiaId,Email,EmailZyrtar,Password,ConfirmPassword,InsertedFrom,InsertedDate,ModifiedDate,ModifiedFrom")] EditStafiViewModel stafiVM)
			 {

				 if (id != stafiVM.Id)
				 {
					 return NotFound();
				 }
				 var staff = _context.Stafi.Where(x => x.Id == id).FirstOrDefault();



				*//* var stafiUpdated = new Stafi();
	 *//*
				 var user = _currentUser.GetCurrentUserName();
			*//*     if (stafiVM.PictureUrl != null)
				 {*//*
					 var result = await _imageService.AddPhotoAsync(stafiVM.PictureUrl);
					  var stafiUpdated = new Stafi()
					   {
						 Id = staff.Id,
						 PictureUrl = result.Url.ToString(),
						 PublicId = result.PublicId,
						 NrLeternjoftimit = stafiVM.NrLeternjoftimit,
						 Emri = staff.Emri,
						 Mbiemri = stafiVM.Mbiemri,
						 Gjinia = staff.Gjinia,
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
						 Password = stafiVM.Password,
						 ConfirmPassword = stafiVM.ConfirmPassword,
						 InsertedFrom = staff.InsertedFrom,
						 InsertedDate = staff.InsertedDate,
						 ModifiedDate = DateTime.Now,
						 ModifiedFrom = user
					 };

				 *//*}
	 *//*
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

			 }*/

		public async Task<IActionResult> Edit(int id, [Bind("Id,NrLeternjoftimit,Emri,Mbiemri,Gjinia,DataLindjes,NrLincences,NrTel,PictureUrl,PublicId,RoletId,ShtetiId,Qyteti,NacionalitetiId,LemiaId,Email,EmailZyrtar,Password,ConfirmPassword,InsertedFrom,InsertedDate,ModifiedDate,ModifiedFrom")] EditStafiViewModel stafiVM)
		{
			if (id != stafiVM.Id)
			{
				return NotFound();
			}

			var staff = _context.Stafi.Where(x => x.Id == id).FirstOrDefault();

			// Detach the already tracked instance of Stafi
			_context.Entry(staff).State = EntityState.Detached;

			var user = _currentUser.GetCurrentUserName();
			var stafiUpdated = new Stafi();
			if (stafiVM.PictureUrl != null)
			{
				var result = await _imageService.AddPhotoAsync(stafiVM.PictureUrl);

				 stafiUpdated = new Stafi()
				{
					Id = staff.Id,
					PictureUrl = result.Url.ToString(),
					PublicId = result.PublicId,
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
					Password = stafiVM.Password,
					ConfirmPassword = stafiVM.ConfirmPassword,
					InsertedFrom = staff.InsertedFrom,
					InsertedDate = staff.InsertedDate,
					ModifiedDate = DateTime.Now,
					ModifiedFrom = user
				};
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


		/*[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,NrLeternjoftimit,Emri,Mbiemri,Gjinia,DataLindjes,NrLincences,NrTel,PictureUrl,PublicId,RoletId,ShtetiId,Qyteti,NacionalitetiId,LemiaId,Email,EmailZyrtar,Password,ConfirmPassword,InsertedFrom,InsertedDate,ModifiedDate,ModifiedFrom")] Stafi stafi)
		{
			if (id != stafi.Id)
			{
				return NotFound();
			}
            var st = new Stafi()
            {
                
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

            _context.Stafi.Remove(stafi);
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