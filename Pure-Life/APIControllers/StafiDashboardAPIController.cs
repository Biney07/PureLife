using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenAI_API.Moderation;
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

		[HttpGet("TerminiMuaji/{stafiId}")]

		public async Task<IActionResult> TerminiMuaji (int stafiId) 
		{

            var terminetEStafit = await _context.Terminet.Where(x => x.Price > 0 && x.StafiId == stafiId).ToListAsync();
            var terminetEPerfunduara = await _context.Terminet.CountAsync(x => x.Price > 0 && x.StafiId == stafiId);

			List<int> muajt = new List<int>();

			for(var i = 1;i<=12;i++)
			{
				muajt.Add(i);
			}

   


            var result = new TerminetMuajiViewModel();
              List<TerminetMuajiViewModel> results = new List<TerminetMuajiViewModel>();

            foreach (var muaji in muajt)
			{
                var monthName = string.Empty;
                switch (muaji)
                {
                    case 1:
                        monthName = "Janar";
                        break;
                    case 2:
                        monthName = "Shkurt";
                        break;
                    case 3:
                        monthName = "Mars";
                        break;
                    case 4:
                        monthName = "Prill";
                        break;
                    case 5:
                        monthName = "Maj";
                        break;
                    case 6:
                        monthName = "Qershor";
                        break;
                    case 7:
                        monthName = "Korrik";
                        break;
                    case 8:
                        monthName = "Gusht";
                        break;
                    case 9:
                        monthName = "Shtator";
                        break;
                    case 10:
                        monthName = "Tetor";
                        break;
                    case 11:
                        monthName = "Nëntor";
                        break;
                    case 12:
                        monthName = "Dhjetor";
                        break;
                    default:
                        monthName = "Invalid month number";
                        break;
                }
                result = new TerminetMuajiViewModel()
                {
                    NumriTermineveTePerfunduara = terminetEStafit.Select(x => DateTime.TryParse(x.StartTime, out DateTime startTime) && startTime.Month == muaji && x.Price > 0).Count() == 0 ? 0 : terminetEStafit.Count(x => DateTime.TryParse(x.StartTime, out DateTime startTime) && startTime.Month == muaji && x.Price > 0),
                    Muaji = monthName
                };

                results.Add(result);
            }

            return Ok(results);

        }

		[HttpGet("FitimetPerMuaj")]

		public async Task<IActionResult> FitimetPerMuaj()
		{

			var terminetEStafit = await _context.Terminet.Where(x => x.Price > 0).ToListAsync();
			var terminetEPerfunduara = await _context.Terminet.CountAsync(x => x.Price > 0);

			List<int> muajt = new List<int>();

			for (var i = 1; i <= 12; i++)
			{
				muajt.Add(i);
			}




			var result = new FitimetMuajiViewModel();
			List<FitimetMuajiViewModel> results = new List<FitimetMuajiViewModel>();

			foreach (var muaji in muajt)
			{
				var monthName = string.Empty;
				switch (muaji)
				{
					case 1:
						monthName = "Janar";
						break;
					case 2:
						monthName = "Shkurt";
						break;
					case 3:
						monthName = "Mars";
						break;
					case 4:
						monthName = "Prill";
						break;
					case 5:
						monthName = "Maj";
						break;
					case 6:
						monthName = "Qershor";
						break;
					case 7:
						monthName = "Korrik";
						break;
					case 8:
						monthName = "Gusht";
						break;
					case 9:
						monthName = "Shtator";
						break;
					case 10:
						monthName = "Tetor";
						break;
					case 11:
						monthName = "Nëntor";
						break;
					case 12:
						monthName = "Dhjetor";
						break;
					default:
						monthName = "Invalid month number";
						break;
				}
				result = new FitimetMuajiViewModel()
				{
					Fitimi = terminetEStafit.Where(x => DateTime.TryParse(x.StartTime, out DateTime startTime) && startTime.Month == muaji && x.Price > 0).Sum(x=>x.Price) == 0 ? 0 :   terminetEStafit.Where(x => DateTime.TryParse(x.StartTime, out DateTime startTime) && startTime.Month == muaji && x.Price > 0).Sum(x => x.Price),
					Muaji = monthName
				};

				results.Add(result);
			}

			return Ok(results);

		}

	}
}
