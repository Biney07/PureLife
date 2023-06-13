using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pure_Life.Data;
using Pure_Life.Models;
using Pure_Life.ViewModel.Terapia;
using System.Globalization;

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

        /*        [HttpPost]
                public async Task<IActionResult> Create(AddTerapiaViewModel model)
                {
                    if (!ModelState.IsValid)
                        return BadRequest(ModelState);

                    var termini = await _context.Terminet
                        .FirstOrDefaultAsync(x => DateTime.Parse(x.StartTime) == DateTime.Parse(model.StartDate) && !x.IsDeleted);

                    if (termini == null)
                        return NotFound("Termini not found");

                    var terapia = new Terapia
                    {
                        Pershkrimi = model.Pershkrimi,
                        Diagnoza = model.Diagnoza,
                        Barnat = model.Barnat,
                        TerminiId = termini.Id
                    };

                    return new JsonResult(terapia);
                }*/
        [HttpPost]
        public async Task<IActionResult> Create(AddTerapiaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime parsedDateTime = DateTime.Parse(model.StartDate);

            var terminiList = await _context.Terminet.ToListAsync();

            var termini = terminiList
                .FirstOrDefault(x => DateTime.Parse(x.StartTime) == parsedDateTime && !x.IsDeleted);

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

            return new JsonResult(terapia);
        }



    }
}
