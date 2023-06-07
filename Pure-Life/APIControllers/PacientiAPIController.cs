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
                NrLeternjoftimit = model.NrLeternjoftimit,
                Emri = model.Emri,
                Mbiemri = model.Mbiemri,
                Gjinia = model.Gjinia,
                DataLindjes = model.DataLindjes,
                Alergji = model.Alergji,
                NrTel = model.NrTel,
                MembershipStatus = model.MembershipStatus,
                ShtetiId = model.ShtetiId,
                Qyteti = model.Qyteti,
                NacionalitetiId = model.NacionalitetiId,
                Email = model.Email,
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword,
                InsertedDate = DateTime.Now,
                InsertedFrom = _currentUser.GetCurrentUserName()
        };

            await _context.Pacientet.AddAsync(pacienti);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var pacientet = _context.Pacientet.ToList();
            return Ok(pacientet);
        }
    }
}
