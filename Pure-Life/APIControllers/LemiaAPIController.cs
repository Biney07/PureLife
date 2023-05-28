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
    public class LemiaAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LemiaAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Lemia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lemia>>> GetLemia()
        {
          if (_context.Lemia == null)
          {
              return NotFound();
          }
            return await _context.Lemia.ToListAsync();
        }

        // GET: api/Lemia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lemia>> GetLemia(int id)
        {
          if (_context.Lemia == null)
          {
              return NotFound();
          }
            var lemia = await _context.Lemia.FindAsync(id);

            if (lemia == null)
            {
                return NotFound();
            }

            return lemia;
        }

        // PUT: api/Lemia/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLemia(int id, Lemia lemia)
        {
            if (id != lemia.Id)
            {
                return BadRequest();
            }

            _context.Entry(lemia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LemiaExists(id))
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

        // POST: api/Lemia
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lemia>> PostLemia(Lemia lemia)
        {
          if (_context.Lemia == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Lemia'  is null.");
          }
            _context.Lemia.Add(lemia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLemia", new { id = lemia.Id }, lemia);
        }

        // DELETE: api/Lemia/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLemia(int id)
        {
            if (_context.Lemia == null)
            {
                return NotFound();
            }
            var lemia = await _context.Lemia.FindAsync(id);
            if (lemia == null)
            {
                return NotFound();
            }

            _context.Lemia.Remove(lemia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LemiaExists(int id)
        {
            return (_context.Lemia?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
