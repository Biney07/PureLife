using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pure_Life.Data;
using Pure_Life.ViewModel.Stafi;

namespace Pure_Life.APIControllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StafiDashboardAPIController : ControllerBase
	{

		private readonly ApplicationDbContext _context;

		public StafiDashboardAPIController(ApplicationDbContext context)
		{
			_context = context;
		}

	

		[HttpGet("StafiStatistikat/{stafiId}")]

		public async Task<IActionResult> StafiStatistikat (int stafiId)
		{
            var pacientet = await _context.Pacientet.Select(x => x.Id).ToListAsync();
            var numriTotalIPacienteve = await _context.Terminet.Where(x => x.StafiId == stafiId && pacientet.Contains(x.Pacienti.Id)).Select(x => x.Pacienti.Id).Distinct().CountAsync();
            var terminetEPerfunduara = await _context.Terminet.CountAsync(x => x.Price > 0 && x.StafiId == stafiId);
            var terminetERezervuara =  await _context.Terminet.CountAsync(x => x.Price == 0 && x.StafiId == stafiId && x.PacientiId != null);

			var result = new StafiDashboardViewModel()
			{
				NumriTotalIPacienteve = numriTotalIPacienteve,
				NumriTermineveTeRezervuara = terminetERezervuara,
				TotaliTermineveEPerfunduara = terminetEPerfunduara,
				TotaliTerapiveTePerfunduara = terminetEPerfunduara
			};

			return Ok(result);	

        }	
			
	}
}
