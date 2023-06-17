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

		[HttpGet("Index")]
		public IActionResult Index()
		{
			var analizat = _context.Sherbimet.ToList();
			return Ok(analizat);
		}
	}
}
