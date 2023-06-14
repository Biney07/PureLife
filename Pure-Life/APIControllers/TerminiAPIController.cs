using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pure_Life.Data;
using Pure_Life.Models;
using Pure_Life.Services;
using Pure_Life.ViewModel.Kujdestarite;
using Pure_Life.ViewModel.Termini;
using Pure_Life.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Pure_Life.APIControllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TerminiAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private ICurrentUser _currentUser;
 



        public TerminiAPIController(ApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
      
        }


		/*        [HttpPost]
				public async Task<IActionResult> Create(int stafiId)
				{
					*//*
					 * 
					 8:00 - 8:30
					 8:30 - 9:00
					 9:00 - 9:30
					 9:30 - 10:00
					 10:30 - 11:00
					 11:00 - 11:30
					11:30 - 12:00

					13:00 - 13:30
					13:30 - 14:00
					14:00 - 14:30
					14:30 - 15:00
					15:30 - 16:00

					 *//*
			   TimeSpan[] timeSlots = new TimeSpan[]
			  {
				new TimeSpan(8, 0, 0),   // 8:00 - 8:30
				new TimeSpan(8, 30, 0),  // 8:30 - 9:00
				new TimeSpan(9, 0, 0),   // 9:00 - 9:30
				new TimeSpan(9, 30, 0),  // 9:30 - 10:00
				new TimeSpan(10, 30, 0), // 10:30 - 11:00
				new TimeSpan(11, 0, 0),  // 11:00 - 11:30
				new TimeSpan(11, 30, 0), // 11:30 - 12:00
				new TimeSpan(13, 0, 0),  // 13:00 - 13:30
				new TimeSpan(13, 30, 0), // 13:30 - 14:00
				new TimeSpan(14, 0, 0),  // 14:00 - 14:30
				new TimeSpan(14, 30, 0), // 14:30 - 15:00
				new TimeSpan(15, 30, 0)  // 15:30 - 16:00
		   };
					//var user = _currentUser.GetCurrentUserName();
					//var stafi = _context.Stafi.Where(x => x.Emri == user).FirstOrDefault();
					var stafi = _context.Stafi.Where(x => x.Id == stafiId).FirstOrDefault();

				  *//*  if (!ModelState.IsValid)
					{
						return BadRequest(model);
					}*//*
					DateTime currentDateTime = DateTime.Now;
					var terminiList = new List<Termini>();

					foreach (var timeSlot in timeSlots)
					{

						var termini = new Termini()
						{
							StartTime = (currentDateTime.Date + timeSlot).ToString(),
							EndTime = (currentDateTime.Date + timeSlot.Add(new TimeSpan(0, 30, 0))).ToString(),
							Status = false,
							Price = 0,
							StafiId = stafi.Id,
							PacientiId = null,
							InsertedDate = currentDateTime,
							InsertedFrom = _currentUser.GetCurrentUserName(),
						};
						terminiList.Add(termini);
					}
					await _context.AddRangeAsync(terminiList);
					await _context.SaveChangesAsync();
					return Ok();

				}*/
		[Route("Create")]
		[HttpPost]
		public async Task<IActionResult> Create(int stafiId)
		{
			TimeSpan[] timeSlots = new TimeSpan[]
			{
		new TimeSpan(8, 0, 0),   // 8:00 - 8:30
        new TimeSpan(8, 30, 0),  // 8:30 - 9:00
        new TimeSpan(9, 0, 0),   // 9:00 - 9:30
        new TimeSpan(9, 30, 0),  // 9:30 - 10:00
        new TimeSpan(10, 30, 0), // 10:30 - 11:00
        new TimeSpan(11, 0, 0),  // 11:00 - 11:30
        new TimeSpan(11, 30, 0), // 11:30 - 12:00
        new TimeSpan(13, 0, 0),  // 13:00 - 13:30
        new TimeSpan(13, 30, 0), // 13:30 - 14:00
        new TimeSpan(14, 0, 0),  // 14:00 - 14:30
        new TimeSpan(14, 30, 0), // 14:30 - 15:00
        new TimeSpan(15, 30, 0)  // 15:30 - 16:00
			};

			var stafi = _context.Stafi.Where(x => x.Id == stafiId).FirstOrDefault();
			DateTime startDate = DateTime.Today; // Assuming today as the starting date
			DateTime endDate = startDate.AddDays(4); // Adding 4 days to include Monday to Friday
			DateTime currentDateTime = DateTime.Now;

			var terminiList = new List<Termini>();

			// Iterate through each weekday
			for (DateTime currentDate = startDate; currentDate <= endDate; currentDate = currentDate.AddDays(1))
			{
				// Iterate through each time slot
				foreach (var timeSlot in timeSlots)
				{
					var termini = new Termini()
					{
						StartTime = (currentDate.Date + timeSlot).ToString(),
						EndTime = (currentDate.Date + timeSlot.Add(new TimeSpan(0, 30, 0))).ToString(),
						Status = false,
						Price = 0,
						StafiId = stafi.Id,
						PacientiId = null,
						InsertedDate = currentDateTime,
						InsertedFrom = _currentUser.GetCurrentUserName(),
					};
					terminiList.Add(termini);
				}
			}

			await _context.AddRangeAsync(terminiList);
			await _context.SaveChangesAsync();
			return Ok();
		}

		[Route("RezervoTerminin")]
		[HttpPut]
		public async Task<IActionResult> RezervoTerminin(int terminiId, int pacientiId)
		{
			var termini = await _context.Terminet.Where(x=>x.Id == terminiId && !x.IsDeleted && x.Status==false).FirstOrDefaultAsync();
			if (termini==null)
			{
				return BadRequest("Terminin nuk u gjet");
			}

			termini.PacientiId = pacientiId;
			termini.Status = true;

			await _context.SaveChangesAsync();
			return Ok("Termini u rezervua me sukses");
		}
		/*[Route("Index")]
		[HttpGet]
        public IActionResult Index()
        {
            var terminet = _context.Terminet.Where(x=>!x.IsDeleted).ToList();
            return Ok(terminet);
        }*/


		[HttpGet]
		public async Task<IActionResult> GetKujdestarite()
		{
			var kujdestarite = await _context.Kujdestarite
				.Include(s => s.Stafi)
				.Where(x => !x.IsDeleted && x.Data.Date >= DateTime.Now.Date)
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
		[Route("Index")]
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var terminet = await _context.Terminet
		.Include(x => x.Pacienti)
		.Where(x => !x.IsDeleted)
		.ToListAsync();

			var result = terminet.Select(x => new GetTerminiViewModel
			{
				Id = x.Id,
				StartTime = x.StartTime,
				EndTime = x.EndTime,
				Status = x.Status ? "I rezervuar" : "I lire",
				PacientiId = x.PacientiId ?? 0,
				PacientiName = x.Pacienti != null ? x.Pacienti.Emri : "null",
				PacientiLastName = x.Pacienti != null ? x.Pacienti.Mbiemri : "null",
				PacientiNrTel = x.Pacienti != null ? x.Pacienti.NrTel: "null",
			});


			return Ok(result);
		}

		[Route("GetTerminiByStaf/{id}")]
        [HttpGet]

        public async Task<IActionResult> GetTerminiByStaf(int id)
        {

            var termini = await _context.Terminet.Where(x=>x.StafiId== id && !x.IsDeleted).ToListAsync();

            return new JsonResult(termini);

        }

        [Route("GetTerminiByDate/{date}")]
        [HttpGet]

        public async Task<IActionResult> GetTerminiByDate(string date)
        {
            DateTime parsedDate = DateTime.Parse(date);
            var terminiList = await _context.Terminet.ToListAsync();
            var termini = terminiList
                .Where(x => DateTime.TryParse(x.StartTime, out DateTime startTime) && startTime.Date == parsedDate && !x.IsDeleted)
                .ToList();

            return new JsonResult(termini);

        }

        [Route("DeleteTermin/{id}")]
        [HttpDelete]

        public async Task<IActionResult> Delete(int id)
        {
            var termini = await _context.Terminet.FindAsync(id);

            if (termini == null)
            {
                return BadRequest("Termini nuk u gjet");
            }

            termini.IsDeleted = true;
            await _context.SaveChangesAsync();
            return new JsonResult("Deleted successfully!");
        }

    }
}
