using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pure_Life.Data;
using Pure_Life.Models;
using Pure_Life.ViewModel;
using Pure_Life.ViewModels;

namespace Pure_Life.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StafiAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StafiAPIController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/StafiAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stafi>>> GetStafi()
        {
          if (_context.Stafi == null)
          {
              return NotFound();
          }
            return await _context.Stafi.ToListAsync();
        }

        // GET: api/StafiAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stafi>> GetStafi(int id)
        {
          if (_context.Stafi == null)
          {
              return NotFound();
          }
            var stafi = await _context.Stafi.FindAsync(id);

            if (stafi == null)
            {
                return NotFound();
            }

            return stafi;
        }

        // PUT: api/StafiAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStafi(int id, Stafi stafi)
        {
            if (id != stafi.Id)
            {
                return BadRequest();
            }

            _context.Entry(stafi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StafiExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StafiAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Stafi>> PostStafi(Stafi stafi)
        {
          if (_context.Stafi == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Stafi'  is null.");
          }
            _context.Stafi.Add(stafi);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStafi", new { id = stafi.Id }, stafi);
        }

		
		[HttpPost("login/{email}/{password}")]
		public async Task<ActionResult<Stafi>> GetStafiByEmailAndPassword(string email, string password)
		{
            var stafi = new Stafi();
			if (email == null || password == null)
			{
				return Problem("Email or password is null.");
			}

			var user = await _userManager.FindByEmailAsync(email);
            
			if (user == null)
			{
				return NotFound();
            }
            else
            {
				var passwordCheck = await _userManager.CheckPasswordAsync(user, password);
				if (!passwordCheck)
				{
					return NotFound();
				}
				else
				{
					stafi = await _context.Stafi.FirstOrDefaultAsync(x => x.Email == user.Email);
				}

			}



			var staf = new Stafi
			{
				Id = stafi.Id,
				NrLeternjoftimit = stafi.NrLeternjoftimit,
				Emri = stafi.Emri,
				Mbiemri = stafi.Mbiemri,
				Gjinia = stafi.Gjinia,
				DataLindjes = stafi.DataLindjes,
				NrLincences = stafi.NrLincences,
				NrTel = stafi.NrTel,
				PictureUrl = stafi.PictureUrl,
				PublicId = stafi.PublicId,
				RoletId = stafi.RoletId,
				ShtetiId = stafi.ShtetiId,
				Qyteti = stafi.Qyteti,
				NacionalitetiId = stafi.NacionalitetiId,
				LemiaId = stafi.LemiaId,
				Email = stafi.Email,
				EmailZyrtar = stafi.EmailZyrtar,
				InsertedFrom = stafi.InsertedFrom,
				InsertedDate = stafi.InsertedDate,
				ModifiedFrom = stafi.ModifiedFrom,
				ModifiedDate = stafi.ModifiedDate
			};

			return staf;

		}


		// DELETE: api/StafiAPI/5
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStafi(int id)
        {
            if (_context.Stafi == null)
            {
                return NotFound();
            }
            var stafi = await _context.Stafi.FindAsync(id);
            if (stafi == null)
            {
                return NotFound();
            }

            _context.Stafi.Remove(stafi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StafiExists(int id)
        {
            return (_context.Stafi?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
