using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pure_Life.Data;
using Pure_Life.Models;

namespace Pure_Life.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShtetiAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ShtetiAPIController(ApplicationDbContext context)
        {
            _context = context;
        }
		// GET: api/ShtetiAPI
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Shteti>>> GetShteti()
		{
			if (_context.Shteti == null)
			{
				return NotFound();
			}
			var shteti = await _context.Shteti.ToListAsync();
			return shteti;
		}
	

		   // GET: api/ShtetiAPI/5
		   [HttpGet("{id}")]
		   public async Task<ActionResult<Shteti>> GetShteti(int id)
		   {
			 if (_context.Shteti == null)
			 {
				 return NotFound();
			 }
			   var shteti = await _context.Shteti.FindAsync(id);

			   if (shteti == null)
			   {
				   return NotFound();
			   }

			   return shteti;
		   }

		   // PUT: api/ShtetiAPI/5
		   // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		   [HttpPut("{id}")]
		   public async Task<IActionResult> PutShteti(int id, Shteti shteti)
		   {
			   if (id != shteti.Id)
			   {
				   return BadRequest();
			   }

			   _context.Entry(shteti).State = EntityState.Modified;

			   try
			   {
				   await _context.SaveChangesAsync();
			   }
			   catch (DbUpdateConcurrencyException)
			   {
				   if (!ShtetiExists(id))
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

		   // POST: api/ShtetiAPI
		   // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		   [HttpPost]
		   public async Task<ActionResult<Shteti>> PostShteti(Shteti shteti)
		   {
			 if (_context.Shteti == null)
			 {
				 return Problem("Entity set 'ApplicationDbContext.Shteti'  is null.");
			 }
			   _context.Shteti.Add(shteti);
			   await _context.SaveChangesAsync();

			   return CreatedAtAction("GetShteti", new { id = shteti.Id }, shteti);
		   }

		   // DELETE: api/ShtetiAPI/5
		   [HttpDelete("{id}")]
		   public async Task<IActionResult> DeleteShteti(int id)
		   {
			   if (_context.Shteti == null)
			   {
				   return NotFound();
			   }
			   var shteti = await _context.Shteti.FindAsync(id);
			   if (shteti == null)
			   {
				   return NotFound();
			   }

			   _context.Shteti.Remove(shteti);
			   await _context.SaveChangesAsync();

			   return NoContent();
		   }

		   private bool ShtetiExists(int id)
		   {
			   return (_context.Shteti?.Any(e => e.Id == id)).GetValueOrDefault();
		   }
	}
}
