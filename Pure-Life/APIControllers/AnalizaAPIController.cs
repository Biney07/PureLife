using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pure_Life.Data;
using Pure_Life.ViewModel.Analizat;

namespace Pure_Life.APIControllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AnalizaAPIController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		public AnalizaAPIController(ApplicationDbContext context)
		{
			_context= context;
		}

		/*[HttpGet]
		public ActionResult<IEnumerable<GetAnalizaAPIViewModel>> GetAnaliza()
		{
			var result = (from tar in _context.TerapiaAnalizaRezultatet
						  join t in _context.Terapia on tar.TerapiaId equals t.Id
						  join ta in _context.TerapiaAnalizaRezultatet on t.Id equals ta.TerapiaId
						  join al in _context.AnalizatLlojet on ta.AnalizaLlojiId equals al.Id
						  join a in _context.Analizat on al.AnalizaId equals a.Id
						  join l in _context.Llojet on al.LlojiId equals l.Id
						  select new GetAnalizaAPIViewModel
						  {
							  Emri = a.Emri,
							  Lloji = l.Emri,
							  Rezultati = tar.Rezultati,
							  VleratReferente = l.VleratReferente
						  }).Distinct().ToList();

			return result;
		}*/
		[HttpGet("{id}/{analizaId}")]
		public ActionResult<IEnumerable<GetAnalizaAPIViewModel>> GetAnaliza(int id ,int analizaId)
		{
			var result = (from tar in _context.TerapiaAnalizaRezultatet
						  join t in _context.Terapia on tar.TerapiaId equals t.Id
						  join ta in _context.TerapiaAnalizaRezultatet on t.Id equals ta.TerapiaId
						  join al in _context.AnalizatLlojet on ta.AnalizaLlojiId equals al.Id
						  join a in _context.Analizat on al.AnalizaId equals a.Id
						  join l in _context.Llojet on al.LlojiId equals l.Id
						  join p in _context.Pacientet on t.Termini.Pacienti.Id equals p.Id
						  where t.Id==id && a.Id== analizaId
						  select new GetAnalizaAPIViewModel
						  {
							  Emri = a.Emri,
							  Lloji = l.Emri,
							  Rezultati = tar.Rezultati,
							  VleratReferente = l.VleratReferente,
							  Pacienti = $"{p.Emri} {p.Mbiemri}",
						  }).Distinct().ToList();

			return result;
		}
	}
}
