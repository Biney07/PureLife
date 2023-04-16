using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pure_Life.Data;
using Pure_Life.Data.Static;
using Pure_Life.Models;
using Pure_Life.ViewModels;

namespace Pure_Life.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public async Task<IActionResult> Index()
        {

           
            // Get a list of users in the role
            var usersWithPermission = _userManager.GetUsersInRoleAsync("ADMIN").Result;

            // Then get a list of the ids of these users
            var idsWithPermission = usersWithPermission.Select(u => u.Id);

            // Now get the users in our database with the same ids
            var users = _context.Users.Where(u => idsWithPermission.Contains(u.Id)).ToList();

            return View(users);


        }
        public AdminController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;

        }
        public IActionResult Create()
        {
            return View();
        }
      
        [HttpPost]
        public async Task<IActionResult> Create(AdminUserViewModel auser)
        {


            var newUser = new ApplicationUser()
            {
                FullName = auser.Name,
                Email = auser.Email,
                UserName = auser.Email
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, auser.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.Admin);
            await _context.SaveChangesAsync();

            //return View("~/Report/Index.cshtml");
            return RedirectToAction(nameof(Index));
            /*   return newUser;*/
        }

		[HttpPost]
		public async Task<IActionResult> DeleteUser(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user != null)
			{
				var roles = await _userManager.GetRolesAsync(user);
				foreach (var role in roles)
				{
					await _userManager.RemoveFromRoleAsync(user, role);
				}
				await _userManager.DeleteAsync(user);
			}
			return RedirectToAction(nameof(Index));
		}

	

	}
}
