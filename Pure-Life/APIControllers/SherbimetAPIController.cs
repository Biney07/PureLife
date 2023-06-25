using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pure_Life.Data;

namespace Pure_Life.APIControllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SherbimetAPIController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		public SherbimetAPIController(ApplicationDbContext context)
		{
			_context= context;
		}

		[HttpGet("Index/{stafiId}")]
		public IActionResult Index(int stafiId)
		{
			var stafi = _context.Stafi.Where(x=>x.Id== stafiId).FirstOrDefault();	
			var sherbimet = _context.Sherbimet.Where(x=>x.LemiaId==stafi.LemiaId || x.LemiaId==null).ToList();
			return Ok(sherbimet);
		}
	}
}
