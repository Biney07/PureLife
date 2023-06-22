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

		[HttpGet("TotaliTermineveEPerfunduara/{stafiId}")]
		public int TotaliTermineveEPerfunduara(int stafiId)
		{
			var terminetCount = _context.Terminet.Count(x => x.Price > 0 && x.StafiId==stafiId);
			return terminetCount;

		}
		[HttpGet("NumriTermineveTeRezervuara/{stafiId}")]
		public int NumriTermineveTeRezervuara(int stafiId)
		{
			var terminetCount = _context.Terminet.Count(x => x.Price == 0 && x.StafiId == stafiId && x.PacientiId!=null);
			return terminetCount;

		}


		[HttpGet("TotaliTerapiveTePerfunduara/{stafiId}")]
		public int TotaliTerapiveTePerfunduara(int stafiId)
		{
			var terminetCount = _context.Terminet.Count(x => x.Price > 0 && x.StafiId == stafiId);
			return terminetCount;

		}
	}
}
