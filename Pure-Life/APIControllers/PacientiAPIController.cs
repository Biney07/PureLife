using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly ImageService _imageService;


        public PacientiAPIController(ApplicationDbContext context, ICurrentUser currentUser, ImageService imageService)
        {
            _context = context;
            _currentUser = currentUser;
            _imageService= imageService;
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
                UId = model.UId,
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

		[HttpGet("GetPacientiByUId/{uId}")]
		public async Task<IActionResult> GetPacientiByUId(string uId)
		{
			var pacienti = await _context.Pacientet.Where(x => x.UId == uId).FirstOrDefaultAsync();

            
			var result =  new GetPacientiViewModel
			{
				Id = pacienti.Id,
				UId = pacienti.UId,
                PictureUrl = pacienti.PictureUrl,
				NrLeternjoftimit = pacienti.NrLeternjoftimit,
				Emri = pacienti.Emri,
				Mbiemri = pacienti.Mbiemri,
				Gjinia = pacienti.Gjinia,
				DataLindjes = pacienti.DataLindjes,
				Alergji = pacienti.Alergji,
				NrTel =pacienti.NrTel,
				MembershipStatus = pacienti.MembershipStatus,
				ShtetiId = pacienti.ShtetiId,
				Qyteti = pacienti.Qyteti,
				NacionalitetiId = pacienti.NacionalitetiId,
				Email = pacienti.Email,
				InsertedDate =pacienti.InsertedDate,
				ModifiedDate = pacienti.ModifiedDate,
				ModifiedFrom =pacienti.ModifiedFrom,
				IsDeleted = pacienti.IsDeleted
			};

			return new JsonResult(result);

		
		}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdatePacientiAPIViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pacienti = await _context.Pacientet.FindAsync(id);

            if (pacienti == null)
            {
                return NotFound();
            }

            if (model.PictureFile != null)
            {
                var result = await _imageService.AddPhotoAsync(model.PictureFile);
                pacienti.PictureUrl = result.Url.ToString();
            }

            // Encapsulate the data
            pacienti.NrLeternjoftimit = model.NrLeternjoftimit;
            pacienti.Emri = model.Emri;
            pacienti.Mbiemri = model.Mbiemri;
            pacienti.Gjinia = model.Gjinia;
            pacienti.DataLindjes = model.DataLindjes;
            pacienti.Alergji = model.Alergji;
            pacienti.NrTel = model.NrTel;
            pacienti.ShtetiId = model.ShtetiId;
            pacienti.Qyteti = model.Qyteti;
            pacienti.NacionalitetiId = model.NacionalitetiId;
          
            pacienti.ModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
