using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pure_Life.Data.Static;
using Pure_Life.Data;
using Pure_Life.Models;

namespace Pure_Life.Services
{
	public class AccountService : IAccountService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ApplicationDbContext _context;
		public AccountService(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
		{
			_userManager = userManager;
			_context = context;
		}

		[HttpPost]
		public async Task RegistersStaf(Stafi stafi)
		{
			var newStaf = new ApplicationUser()
			{
				FullName = stafi.Emri,
				Email = stafi.Email,
				UserName = stafi.Email


			};
			var newUserResponse = await _userManager.CreateAsync(newStaf, stafi.Password);

			if (newUserResponse.Succeeded)
				await _userManager.AddToRoleAsync(newStaf, UserRoles.User);
			await _context.SaveChangesAsync();
		}
	}
}
