using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pure_Life.Data;
using Pure_Life.Models;
using Pure_Life.Services;
using Pure_Life.ViewModel.Pacienti;

namespace Pure_Life.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientiAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private ICurrentUser _currentUser;

        public PacientiAPIController(ApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddPacientiAPIViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            var pacienti = new Pacienti()
            {
                UId = null,
                Emri = model.Emri,
                Mbiemri = model.Mbiemri,
                MembershipStatus = false,
                Email = model.Email,
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword,
                InsertedDate = DateTime.Now,
              
        };

            await _context.Pacientet.AddAsync(pacienti);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var pacientet = _context.Pacientet.ToList();
			var result = pacientet.Select(x => new GetPacientiViewModel
			{
				Id = x.Id,
				UId = x.UId,
				NrLeternjoftimit = x.NrLeternjoftimit,
				Emri = x.Emri,
				Mbiemri = x.Mbiemri,
				Gjinia = x.Gjinia,
				DataLindjes = x.DataLindjes,
				Alergji = x.Alergji,
				NrTel = x.NrTel,
				MembershipStatus = x.MembershipStatus,
				ShtetiId = x.ShtetiId,
				Qyteti = x.Qyteti,
				NacionalitetiId = x.NacionalitetiId,
				Email = x.Email,
				InsertedDate = x.InsertedDate,
				ModifiedDate = x.ModifiedDate,
				ModifiedFrom = x.ModifiedFrom,
				IsDeleted = x.IsDeleted
			});


			return Ok(pacientet);
        }
    }
}
