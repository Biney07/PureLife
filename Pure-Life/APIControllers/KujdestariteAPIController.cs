using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pure_Life.Data;
using Pure_Life.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pure_Life.Models;
using System.Web.Helpers;
using Pure_Life.ViewModel.Kujdestarite;

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
			var kujdestarite = await _context.Kujdestarite
				.Include(s=>s.Stafi)
				.Where(x=>!x.IsDeleted && x.Data.Date>=DateTime.Now.Date)
				.ToListAsync();
			
			if (_context.Kujdestarite == null)
			{
				return BadRequest("test error");
			}
			
			var result = kujdestarite.Select(x => new GetKujdestariteAPIViewModel
			{
				Id = x.Id,
				Data = x.Data,
				Kati = x.Kati,
				Reparti = x.Reparti,
				StafiEmriMbiemri = $"{x.Stafi.Emri} {x.Stafi.Mbiemri}",
				StafiId = x.StafiId,
			});
			return Ok(result);
		}





	}
}
