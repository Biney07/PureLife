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
using Pure_Life.ViewModel;

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
        public async Task<IActionResult> Create(StafiViewModel viewModel)
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




            return View(viewModel);
        }
       



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

            return RedirectToAction(nameof(Index));
        }

    }
}