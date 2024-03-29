﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList;
using Pure_Life.Data;

using Pure_Life.Models;
using Pure_Life.Models.Analiza;
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
			_context = context;
		}
	
		[HttpPost]
		public async Task<IActionResult> Create(AddTerapiaViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var termini = _context.Terminet
				.FirstOrDefault(x => x.Id == model.TerminiId && !x.IsDeleted && x.HasTherapy==false);

			if (termini == null)
			{
				return NotFound("Termini not found");
			}
            List<int> analizaIds = new List<int>();	

            List<TerapiaAnalizaRezultati> terapiaAnalizaRezultatet = new List<TerapiaAnalizaRezultati>();

			if (!string.IsNullOrEmpty(model.AnalizatECaktuara))
			{
				 analizaIds = model.AnalizatECaktuara
		.Split(',')
		.Select(x => int.TryParse(x.Trim(), out int parsedId) ? parsedId : 0)
		.Where(x => x != 0)
		.ToList();

				terapiaAnalizaRezultatet = await _context.AnalizatLlojet
					.Where(x => analizaIds.Contains(x.AnalizaId))
					.Select(x => new TerapiaAnalizaRezultati
					{
						AnalizaLlojiId = x.Id
						//TerapiaId //kjo veqse mbushet ma poshte kur te krijohet terapia
						//Rezultati // rezultati mbushet mas anej kurdo qe te osht nevoja
					})
					.ToListAsync();
			}
		
			List<int> sherbimetIds = new List<int>();

			foreach (var sherbimId in model.SherbimetEKryera.Split(','))
			{
				var cleanedId = new string(sherbimId.Where(char.IsDigit).ToArray());

				if (!string.IsNullOrEmpty(cleanedId) && int.TryParse(cleanedId, out int id))
				{
					sherbimetIds.Add(id);
				}
			}

			List<TerapiaSherbimet> terapiaSherbimet = sherbimetIds.Select(sherbimId => new TerapiaSherbimet
			{
				SherbimetId = sherbimId
			}).ToList();

			var stafi = await _context.Stafi.Where(x => x.Id == termini.StafiId).FirstOrDefaultAsync();
			var terapia = new Terapia()
			{
				Pershkrimi = model.Pershkrimi,
				Diagnoza = model.Diagnoza,
				Barnat = model.Barnat,
				TerminiId = termini.Id,
				InsertedDate = DateTime.Now,
				InsertedFrom = stafi.EmailZyrtar,
				TerapiaSherbimet = terapiaSherbimet,
				TerapiaAnalizaRezultati = terapiaAnalizaRezultatet
			};

			await _context.Terapia.AddAsync(terapia);
			await _context.SaveChangesAsync();

			decimal totalPrice = await _context.Sherbimet
				.Where(s => sherbimetIds.Contains(s.Id))
				.SumAsync(s => s.Cmimi);


			double totalPriceAnalizat =await _context.Analizat
				.Where(s => analizaIds.Contains(s.Id))
				.SumAsync(s => s.Cmimi);

			var shuma = totalPrice + (decimal) totalPriceAnalizat;

			var terminiPrice = await _context.Terminet.FirstOrDefaultAsync(x => x.Id == termini.Id);
			terminiPrice.Price = (double)shuma;
			terminiPrice.HasTherapy = true;
			await _context.SaveChangesAsync();

			var terapiaa = await _context.Terapia
				.Include(t => t.Termini)
					.ThenInclude(t => t.Stafi)
				.Include(t => t.Termini)
					.ThenInclude(t => t.Pacienti)
				.Where(x => x.Id == terapia.Id)
				.FirstOrDefaultAsync();

			var analizatESapoKryera = _context.TerapiaAnalizaRezultatet.Include(x => x.AnalizaLloji).ThenInclude(x => x.Analiza).Where(x => terapiaAnalizaRezultatet.Select(x => x.Id).Contains(x.Id)).ToList();

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
				/*	Analizat = analizatESapoKryera.Select(x=>x.AnalizaLloji.Analiza).Distinct().Select(x=> new AnalizaETerapise
					{
						Id = x.Id,
						EmriAnalizes = x.Emri,
						Cmimi = x.Cmimi,
					}).ToList(),*/
				Analizat = analizatESapoKryera
	.Select(x => x.AnalizaLloji?.Analiza)
	.Where(a => a != null)
	.Distinct()
	.Select(a => new AnalizaETerapise
	{
		Id = a.Id,
		EmriAnalizes = a.Emri,
		Cmimi = a.Cmimi
	})
	.ToList(),
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
			var result = await _context.Terapia.Select(x => new GetTerapiaViewModel
			{
				Id = x.Id,
				Pacienti = $"{x.Termini.Pacienti.Emri} {x.Termini.Pacienti.Mbiemri}",
				NrLeternjoftimit = x.Termini.Pacienti.NrLeternjoftimit,
				Koha = $"{x.Termini.StartTime} - {x.Termini.EndTime}",
				Diagnoza = x.Diagnoza,
				Pershkrimi = x.Pershkrimi,
				Barnat = x.Barnat,
				Cmimi = x.Termini.Price,
				TerminiId = x.TerminiId,
				Sherbimet = x.TerapiaSherbimet != null
				? x.TerapiaSherbimet.Select(s => s.Sherbimet.Emri).ToList() : null,
				Doktori = $"Dr {x.Termini.Stafi.Emri} {x.Termini.Stafi.Mbiemri}",
				InsertedFrom = x.InsertedFrom,
				InsertedDate = x.InsertedDate,
				ModifiedDate = x.ModifiedDate,
				ModifiedFrom = x.ModifiedFrom,
				Analizat = x.TerapiaAnalizaRezultati.Select(y=> y.AnalizaLloji.Analiza).Distinct().Select(y=> new AnalizaETerapise
				{
					Id = y.Id,
					EmriAnalizes = y.Emri,
					Cmimi = y.Cmimi,
					Data = y.Data
				}).ToList()
			}).ToListAsync();

			return Ok(result);
		}

		[HttpGet("GetAnalizenETerapise/{analizaId}/{terapiaId}")]

		public async Task<IActionResult> GetAnalizenETerapise (int terapiaId, int analizaId)
		{
			var result = await _context.Terapia.Where(x => x.Id == terapiaId).Select(x => new RezultatetEAnalizaveViewModel
			{
                EmriAnalizes = x.TerapiaAnalizaRezultati.Where(y=> y.AnalizaLloji.AnalizaId == analizaId).Select(y=> y.AnalizaLloji.Analiza.Emri).FirstOrDefault(),
				EmriMbiemriPacientit = $"{x.Termini.Pacienti.Emri} {x.Termini.Pacienti.Mbiemri}",
				Data = x.Termini.StartTime.ToString(),
				ListaRezultateve = x.TerapiaAnalizaRezultati.Where(y => y.AnalizaLloji.AnalizaId == analizaId).Select(y=> new ListaRezultateve{
                    TerapiaAnalizaRezultatiId = y.Id, //kur don me ba update rezultatin e dergon qet id, edhe ne baze te saj e merr prej tabeles TerapiaAnalizaRezultati.Where(x=> x.Id == TerapiaAnalizaRezultatiId).First() edhe e bon update Rezultati
                    EmriLlojitTeAnalizes = y.AnalizaLloji.Lloji.Emri,
					Rezultati = y.Rezultati,
					VleratReferente = y.AnalizaLloji.Lloji.VleratReferente
                }).ToList(),
            }).FirstOrDefaultAsync();

			return Ok(result);
        }

		[HttpPut("FillTheResult/{terapiaRezultatiId}/{rezultatiShkruar}")]
		public async Task<IActionResult> FillTheResult(int terapiaRezultatiId,string rezultatiShkruar)
		{
			var rezultati = await _context.TerapiaAnalizaRezultatet.Where(x=>x.Id == terapiaRezultatiId).FirstOrDefaultAsync();
			rezultati.Rezultati = rezultatiShkruar;
			await _context.SaveChangesAsync();
			return Ok(rezultati);
		}
	

		[HttpPut("Edit/{id}/{stafiId}")]
		public async Task<IActionResult> Edit(int id, EditTerapiaViewModel model, int stafiId)
		{
			var terapia = await _context.Terapia
			.Include(t => t.Termini)
				.ThenInclude(t => t.Stafi)
			.Include(t => t.Termini)
				.ThenInclude(t => t.Pacienti)
			.Where(x => x.Id == id)
			.FirstOrDefaultAsync();

			if (terapia == null)
			{
				return BadRequest("Terapia nuk u gjet");
			}
			string[] sherbimetIds = model.SherbimetEKryera.Split(',');
			List<int> sherbimetIdList = sherbimetIds.Select(int.Parse).ToList();

			terapia.Id = id;
			terapia.Pershkrimi = model.Pershkrimi;
			terapia.Diagnoza = model.Diagnoza;
			terapia.Barnat = model.Barnat;
		
			var newEntries = sherbimetIdList.Select(sherbimetId => new TerapiaSherbimet
			{
				TerapiaId = id,
				SherbimetId = sherbimetId
			}).ToList();

			_context.TerapiaSherbimet.AddRange(newEntries);

			terapia.ModifiedFrom = _context.Stafi.FirstOrDefault(x => x.Id == stafiId).EmailZyrtar;
			terapia.ModifiedDate = DateTime.Now;

			await _context.SaveChangesAsync();

			var terapite = await _context.Terapia
.Include(t => t.Termini)
.ThenInclude(t => t.Stafi)
.Include(t => t.Termini)
.ThenInclude(t => t.Pacienti)
.Include(t => t.TerapiaSherbimet)
.ThenInclude(ts => ts.Sherbimet)
.Where(t => t.Id == terapia.Id)
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
				/*				SherbimetObj = x.TerapiaSherbimet != null
					? x.TerapiaSherbimet.Select(s => new Pure_Life.ViewModel.Terapia.Sherbimet
					{
						Id = s.Sherbimet.Id,
						Emri = s.Sherbimet.Emri
					}).ToList()
					: null,*/


				/*SherbimetIds = x.TerapiaSherbimet.*/
				Doktori = $"Dr {x.Termini.Stafi.Emri} {x.Termini.Stafi.Mbiemri}",
				InsertedFrom = x.InsertedFrom,
				InsertedDate = x.InsertedDate,
				ModifiedDate = x.ModifiedDate,
				ModifiedFrom = x.ModifiedFrom
			});
			return Ok(result);



		}



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
				TerminiId = x.TerminiId,
				Cmimi = x.Termini.Price,
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

		[HttpPut("DeleteTerapia/{id}")]

		public async Task<IActionResult> DeleteTerapia(int id)
		{
			var terapia = await _context.Terapia
			.Include(t => t.Termini)
				.ThenInclude(t => t.Stafi)
			.Include(t => t.Termini)
				.ThenInclude(t => t.Pacienti)
			.Where(x => x.Id == id)
			.FirstOrDefaultAsync();

			if(terapia == null)
			{
				return BadRequest("Terapia nuk u gjet");
			}
			terapia.IsDeleted = true;
			await _context.SaveChangesAsync();

			var terapite = await _context.Terapia
				.Include(t => t.Termini)
					.ThenInclude(t => t.Stafi)
				.Include(t => t.Termini)
					.ThenInclude(t => t.Pacienti)
				.Include(t => t.TerapiaSherbimet)
					.ThenInclude(t => t.Sherbimet)
				.Where(t => t.Id == id)
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
				TerminiId = x.TerminiId,
				Cmimi = x.Termini.Price,
				Sherbimet = x.TerapiaSherbimet != null
		? x.TerapiaSherbimet.Select(s => s.Sherbimet.Emri).ToList() : null,
				Doktori = $"Dr {x.Termini.Stafi.Emri} {x.Termini.Stafi.Mbiemri}",
				InsertedFrom = x.InsertedFrom,
				InsertedDate = x.InsertedDate,
				ModifiedDate = x.ModifiedDate,
				ModifiedFrom = x.ModifiedFrom,
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
				.Where(t => t.Termini.Pacienti.NrLeternjoftimit == leternjoftimi)
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
				TerminiId = x.TerminiId,
				Cmimi = x.Termini.Price,
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
				.Where(t => t.Termini.Stafi.Id == id && t.IsDeleted == false)
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
				TerminiId = x.TerminiId,
				Cmimi = x.Termini.Price,
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
				Pershkrimi = terapite.Pershkrimi,
				Barnat = terapite.Barnat,
				TerminiId = terapite.TerminiId,
				Cmimi = terapite.Termini.Price,
				Doktori = $"Dr {terapite.Termini.Stafi.Emri} {terapite.Termini.Stafi.Mbiemri}",
				InsertedFrom = terapite.InsertedFrom,
				InsertedDate = terapite.InsertedDate,
				ModifiedDate = terapite.ModifiedDate,
				ModifiedFrom = terapite.ModifiedFrom
			};
			return Ok(result);
		}


		[HttpGet("GetTerapiaByTermin/{id}")]
		public async Task<IActionResult> GetTerapiaByTermin(int id)
		{
			var terapia = await _context.Terapia
				.Include(t => t.Termini)
					.ThenInclude(t => t.Stafi)
				.Include(t => t.Termini)
					.ThenInclude(t => t.Pacienti)
				.Include(t => t.TerapiaSherbimet)
					.ThenInclude(t => t.Sherbimet)
				.Include(t => t.TerapiaAnalizaRezultati)
					.ThenInclude(t => t.AnalizaLloji)
						.ThenInclude(a => a.Analiza)
				.Where(t => t.TerminiId == id)
				.FirstOrDefaultAsync();

			if (terapia == null)
			{
				return NotFound(); // Return appropriate response when terapia is not found
			}

			var result = new GetTerapiaViewModel
			{
				Id = terapia.Id,
				Pacienti = $"{terapia.Termini.Pacienti.Emri} {terapia.Termini.Pacienti.Mbiemri}",
				NrLeternjoftimit = terapia.Termini.Pacienti.NrLeternjoftimit,
				Koha = $"{terapia.Termini.StartTime} - {terapia.Termini.EndTime}",
				Diagnoza = terapia.Diagnoza,
				Pershkrimi = terapia.Pershkrimi,
				Barnat = terapia.Barnat,
				Cmimi = terapia.Termini.Price,
				TerminiId = id,
				Doktori = $"Dr {terapia.Termini.Stafi.Emri} {terapia.Termini.Stafi.Mbiemri}",
				InsertedFrom = terapia.InsertedFrom,
				InsertedDate = terapia.InsertedDate,
				ModifiedDate = terapia.ModifiedDate,
				ModifiedFrom = terapia.ModifiedFrom,
				Sherbimet = terapia.TerapiaSherbimet != null
					? terapia.TerapiaSherbimet.Select(s => s.Sherbimet.Emri).ToList()
					: null,
				Analizat = terapia.TerapiaAnalizaRezultati.Select(y => y.AnalizaLloji.Analiza).Distinct().Select(terapia => new AnalizaETerapise
				{
					Id = terapia.Id,
					EmriAnalizes = terapia.Emri,
					Cmimi = terapia.Cmimi,
					Data = terapia.Data
				}).ToList()
			};

			

			return Ok(result);
		}

	}
}
