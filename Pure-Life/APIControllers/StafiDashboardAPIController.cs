using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pure_Life.Data;

namespace Pure_Life.APIControllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StafiDashboardAPIController : ControllerBase
	{

		private readonly ApplicationDbContext _context;

		public StafiDashboardAPIController(ApplicationDbContext context)
		{
			_context= context;
		}

		[HttpGet("NumriTotalIPacienteve/{stafiId}")]
		public int NumriTotalIPacienteve(int stafiId)
		{
			var pacientet = _context.Pacientet.Select(x => x.Id).ToList();
			var count = _context.Terminet.Where(x => x.StafiId == stafiId && pacientet.Contains(x.Pacienti.Id)).Select(x => x.Pacienti.Id).Distinct().Count();
			return count;

		}
	}
}
