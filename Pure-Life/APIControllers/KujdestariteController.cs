using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pure_Life.Data;
using Pure_Life.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pure_Life.Models;
using System.Web.Helpers;

namespace Pure_Life.APIControllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class KujdestariteController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		public KujdestariteController(ApplicationDbContext context)
		{
			_context = context;
		}


		[HttpGet]
		public async Task<IActionResult> GetKujdestarite()
		{
			var kujdestarite = await _context.Kujdestarite.ToListAsync();
			if (_context.Kujdestarite == null)
			{
				return BadRequest("test error");
			}
			return Ok(kujdestarite);
		}





	}
}
