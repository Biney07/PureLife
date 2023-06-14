﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;
using Pure_Life.Data;
using Pure_Life.Migrations;
using Pure_Life.Models;
using Pure_Life.ViewModel.Terapia;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Pure_Life.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerapiaAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TerapiaAPIController(ApplicationDbContext context)
        {
            _context= context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddTerapiaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			/*   DateTime parsedDateTime = DateTime.Parse(model.StartDate);

			   var terminiList = await _context.Terminet.ToListAsync();*/

			/*      var termini = terminiList
					  .FirstOrDefault(x => DateTime.Parse(x.StartTime) == parsedDateTime && !x.IsDeleted);*/
			var termini = _context.Terminet
				.FirstOrDefault(x => x.Id == model.TerminiId && !x.IsDeleted);

			if (termini == null)
            {
                return NotFound("Termini not found");
            }
            var stafi = await _context.Stafi.Where(x=>x.Id==termini.StafiId).FirstOrDefaultAsync();
            var terapia = new Terapia()
            {
                Pershkrimi = model.Pershkrimi,
                Diagnoza = model.Diagnoza,
                Barnat = model.Barnat,
                TerminiId = termini.Id,
                InsertedDate = DateTime.Now,
                InsertedFrom = stafi.EmailZyrtar
            };

			List<int> sherbimetIds = new List<int>();

			foreach (var sherbimId in model.SherbimetEKryera.Split(','))
			{
				var cleanedId = new string(sherbimId.Where(char.IsDigit).ToArray());

				if (!string.IsNullOrEmpty(cleanedId) && int.TryParse(cleanedId, out int id))
				{
					sherbimetIds.Add(id);
				}
			}

			await _context.Terapia.AddAsync(terapia);
			await _context.SaveChangesAsync();

			List<TerapiaSherbimet> terapiaSherbimet = sherbimetIds.Select(sherbimId => new TerapiaSherbimet
			{
				TerapiaId = terapia.Id,
				SherbimetId = sherbimId
			}).ToList();

			decimal totalPrice = await _context.Sherbimet
	.Where(s => sherbimetIds.Contains(s.Id))
	.SumAsync(s => s.Cmimi);

			var terminiPrice = await _context.Terminet.FirstOrDefaultAsync(x => x.Id == termini.Id);
			terminiPrice.Price = (double)totalPrice;

			await _context.TerapiaSherbimet.AddRangeAsync(terapiaSherbimet);
			await _context.SaveChangesAsync();


		


			var terapiaa = await _context.Terapia
				.Include(t => t.Termini)
					.ThenInclude(t => t.Stafi)
				.Include(t => t.Termini)
					.ThenInclude(t => t.Pacienti)
				.Where(x=>x.Id==terapia.Id)
				.FirstOrDefaultAsync();
			
			var result = new GetTerapiaViewModel
			{
				Id = terapiaa.Id,
				Pacienti = $"{terapiaa.Termini.Pacienti.Emri} {terapiaa.Termini.Pacienti.Mbiemri}",
				NrLeternjoftimit = terapiaa.Termini.Pacienti.NrLeternjoftimit,
				Koha = $"{terapiaa.Termini.StartTime} - {terapiaa.Termini.EndTime}",
				Diagnoza = terapiaa.Diagnoza,
				Pershkrimi = terapiaa.Pershkrimi,
				Barnat = terapiaa.Barnat,
				Sherbimet = await _context.Sherbimet
				.Where(s => sherbimetIds.Contains(s.Id))
				.Select(s => s.Emri)
				.ToListAsync(),
				Doktori = $"Dr {terapiaa.Termini.Stafi.Emri} {terapiaa.Termini.Stafi.Mbiemri}",
				InsertedFrom = terapiaa.InsertedFrom,
				InsertedDate = terapiaa.InsertedDate,
				ModifiedDate = terapiaa.ModifiedDate,
				ModifiedFrom = terapiaa.ModifiedFrom
			};
			return Ok(result);
		}

      

		[HttpGet("GetTerapiteEPacienteveMeSherbime")]
		public async Task<IActionResult> GetTerapiteEPacienteveMeSherbime()
		{



			var terapite = await _context.Terapia
	.Include(t => t.Termini)
		.ThenInclude(t => t.Stafi)
	.Include(t => t.Termini)
		.ThenInclude(t => t.Pacienti)
	.Include(t => t.TerapiaSherbimet)
		.ThenInclude(ts => ts.Sherbimet)
	.ToListAsync();


			var result = terapite.Select(x => new GetTerapiaViewModel
			{
				Id = x.Id,
				Pacienti = $"{x.Termini.Pacienti.Emri} {x.Termini.Pacienti.Mbiemri}",
				NrLeternjoftimit = x.Termini.Pacienti.NrLeternjoftimit,
				Koha = $"{x.Termini.StartTime} - {x.Termini.EndTime}",
				Diagnoza = x.Diagnoza,
				Pershkrimi = x.Pershkrimi,
				Barnat = x.Barnat,
				Sherbimet = x.TerapiaSherbimet != null 
				? x.TerapiaSherbimet.Select(s=>s.Sherbimet.Emri).ToList() : null,
				Doktori = $"Dr {x.Termini.Stafi.Emri} {x.Termini.Stafi.Mbiemri}",
				InsertedFrom = x.InsertedFrom,
				InsertedDate = x.InsertedDate,
				ModifiedDate = x.ModifiedDate,
				ModifiedFrom = x.ModifiedFrom
			});
			return Ok(result);
		}

		/*[HttpGet("GetTerapiteEPacienteve")]

		public async Task<IActionResult> GetTerapiteEPacienteve()
		{



			var terapite = await _context.Terapia
				.Include(t => t.Termini)
					.ThenInclude(t => t.Stafi)
				.Include(t => t.Termini)
					.ThenInclude(t => t.Pacienti)
				.ToListAsync();

			var result = terapite.Select(x => new GetTerapiaViewModel
			{
				Id = x.Id,
				Pacienti = $"{x.Termini.Pacienti.Emri} {x.Termini.Pacienti.Mbiemri}",
				NrLeternjoftimit = x.Termini.Pacienti.NrLeternjoftimit,
				Koha = $"{x.Termini.StartTime} - {x.Termini.EndTime}",
				Diagnoza = x.Diagnoza,
				Pershkrimi = x.Pershkrimi,
				Barnat = x.Barnat,
				Doktori = $"Dr {x.Termini.Stafi.Emri} {x.Termini.Stafi.Mbiemri}",
				InsertedFrom = x.InsertedFrom,
				InsertedDate = x.InsertedDate,
				ModifiedDate = x.ModifiedDate,
				ModifiedFrom = x.ModifiedFrom
			});
			return Ok(result);
		}*/

		[HttpGet("GetTerapiteEPacientitID/{id}")]

		public async Task<IActionResult> GetTerapiteEPacientitID(int id)
		{



			var terapite = await _context.Terapia
				.Include(t => t.Termini)
					.ThenInclude(t => t.Stafi)
				.Include(t => t.Termini)
					.ThenInclude(t => t.Pacienti)
				.Include(t => t.TerapiaSherbimet)
					.ThenInclude(t => t.Sherbimet)
				.Where(t => t.Termini.PacientiId == id)
				.ToListAsync();

			var result = terapite.Select(x => new GetTerapiaViewModel
			{
				Id = x.Id,
				Pacienti = $"{x.Termini.Pacienti.Emri} {x.Termini.Pacienti.Mbiemri}",
				NrLeternjoftimit = x.Termini.Pacienti.NrLeternjoftimit,
				Koha = $"{x.Termini.StartTime} - {x.Termini.EndTime}",
				Diagnoza = x.Diagnoza,
				Pershkrimi = x.Pershkrimi,
				Barnat = x.Barnat,
				Sherbimet = x.TerapiaSherbimet != null
			? x.TerapiaSherbimet.Select(s => s.Sherbimet.Emri).ToList() : null,
				Doktori = $"Dr {x.Termini.Stafi.Emri} {x.Termini.Stafi.Mbiemri}",
				InsertedFrom = x.InsertedFrom,
				InsertedDate = x.InsertedDate,
				ModifiedDate = x.ModifiedDate,
				ModifiedFrom = x.ModifiedFrom
			});
			return Ok(result);
		}

		[HttpGet("GetTerapiteEPacientitLeternjoftim/{leternjoftimi}")]

		public async Task<IActionResult> GetTerapiteEPacientitLeternjoftim(string leternjoftimi)
		{



			var terapite = await _context.Terapia
				.Include(t => t.Termini)
					.ThenInclude(t => t.Stafi)
				.Include(t => t.Termini)
					.ThenInclude(t => t.Pacienti).
				Include(t => t.TerapiaSherbimet)
					.ThenInclude(t => t.Sherbimet)
				.Where(t => t.Termini.Pacienti.NrLeternjoftimit==leternjoftimi)
				.ToListAsync();

			var result = terapite.Select(x => new GetTerapiaViewModel
			{
				Id = x.Id,
				Pacienti = $"{x.Termini.Pacienti.Emri} {x.Termini.Pacienti.Mbiemri}",
				NrLeternjoftimit = x.Termini.Pacienti.NrLeternjoftimit,
				Koha = $"{x.Termini.StartTime} - {x.Termini.EndTime}",
				Diagnoza = x.Diagnoza,
				Pershkrimi = x.Pershkrimi,
				Barnat = x.Barnat,
				Sherbimet = x.TerapiaSherbimet != null
				? x.TerapiaSherbimet.Select(s => s.Sherbimet.Emri).ToList() : null,
				Doktori = $"Dr {x.Termini.Stafi.Emri} {x.Termini.Stafi.Mbiemri}",
				InsertedFrom = x.InsertedFrom,
				InsertedDate = x.InsertedDate,
				ModifiedDate = x.ModifiedDate,
				ModifiedFrom = x.ModifiedFrom
			});
			return Ok(result);
		}
		[HttpGet("GetTerapiteWrittenByStaff/{id}")]

		public async Task<IActionResult> GetTerapiteWrittenByStaff(int id)
		{



			var terapite = await _context.Terapia
				.Include(t => t.Termini)
					.ThenInclude(t => t.Stafi)
				.Include(t => t.Termini)
					.ThenInclude(t => t.Pacienti)
				.Include(t => t.TerapiaSherbimet)
					.ThenInclude(t => t.Sherbimet)
				.Where(t => t.Termini.Stafi.Id == id)
				.ToListAsync();

			var result = terapite.Select(x => new GetTerapiaViewModel
			{
				Id = x.Id,
				Pacienti = $"{x.Termini.Pacienti.Emri} {x.Termini.Pacienti.Mbiemri}",
				NrLeternjoftimit = x.Termini.Pacienti.NrLeternjoftimit,
				Koha = $"{x.Termini.StartTime} - {x.Termini.EndTime}",
				Diagnoza = x.Diagnoza,
				Pershkrimi = x.Pershkrimi,
				Barnat = x.Barnat,
				Sherbimet = x.TerapiaSherbimet != null
			? x.TerapiaSherbimet.Select(s => s.Sherbimet.Emri).ToList() : null,
				Doktori = $"Dr {x.Termini.Stafi.Emri} {x.Termini.Stafi.Mbiemri}",
				InsertedFrom = x.InsertedFrom,
				InsertedDate = x.InsertedDate,
				ModifiedDate = x.ModifiedDate,
				ModifiedFrom = x.ModifiedFrom
			});
			return Ok(result);
		}

		[HttpGet("GetTerapiaById/{id}")]

		public async Task<IActionResult> GetTerapiaById(int id)
		{



			var terapite = await _context.Terapia
				.Include(t => t.Termini)
					.ThenInclude(t => t.Stafi)
				.Include(t => t.Termini)
					.ThenInclude(t => t.Pacienti)
				.Where(t => t.Id == id)
				.FirstOrDefaultAsync();

			var result = new GetTerapiaViewModel
			{
				Id = terapite.Id,
				Pacienti = $"{terapite.Termini.Pacienti.Emri} {terapite.Termini.Pacienti.Mbiemri}",
				NrLeternjoftimit = terapite.Termini.Pacienti.NrLeternjoftimit,
				Koha = $"{terapite.Termini.StartTime} - {terapite.Termini.EndTime}",
				Diagnoza = terapite.Diagnoza,
				Pershkrimi =terapite.Pershkrimi,
				Barnat = terapite.Barnat,
				Doktori = $"Dr {terapite.Termini.Stafi.Emri} {terapite.Termini.Stafi.Mbiemri}",
				InsertedFrom = terapite.InsertedFrom,
				InsertedDate = terapite.InsertedDate,
				ModifiedDate = terapite.ModifiedDate,
				ModifiedFrom = terapite.ModifiedFrom
			};
			return Ok(result);
		}

	}
}