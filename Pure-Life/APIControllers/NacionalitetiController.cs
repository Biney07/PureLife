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
    public class NacionalitetiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NacionalitetiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Nacionaliteti
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nacionaliteti>>> GetNacionaliteti()
        {
          if (_context.Nacionaliteti == null)
          {
              return NotFound();
          }
            return await _context.Nacionaliteti.ToListAsync();
        }

        // GET: api/Nacionaliteti/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Nacionaliteti>> GetNacionaliteti(int id)
        {
          if (_context.Nacionaliteti == null)
          {
              return NotFound();
          }
            var nacionaliteti = await _context.Nacionaliteti.FindAsync(id);

            if (nacionaliteti == null)
            {
                return NotFound();
            }

            return nacionaliteti;
        }

        // PUT: api/Nacionaliteti/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNacionaliteti(int id, Nacionaliteti nacionaliteti)
        {
            if (id != nacionaliteti.Id)
            {
                return BadRequest();
            }

            _context.Entry(nacionaliteti).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NacionalitetiExists(id))
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

        // POST: api/Nacionaliteti
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Nacionaliteti>> PostNacionaliteti(Nacionaliteti nacionaliteti)
        {
          if (_context.Nacionaliteti == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Nacionaliteti'  is null.");
          }
            _context.Nacionaliteti.Add(nacionaliteti);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNacionaliteti", new { id = nacionaliteti.Id }, nacionaliteti);
        }

        // DELETE: api/Nacionaliteti/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNacionaliteti(int id)
        {
            if (_context.Nacionaliteti == null)
            {
                return NotFound();
            }
            var nacionaliteti = await _context.Nacionaliteti.FindAsync(id);
            if (nacionaliteti == null)
            {
                return NotFound();
            }

            _context.Nacionaliteti.Remove(nacionaliteti);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NacionalitetiExists(int id)
        {
            return (_context.Nacionaliteti?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
