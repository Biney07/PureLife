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
    public class RoletController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RoletController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Rolet
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rolet>>> GetRolet()
        {
          if (_context.Rolet == null)
          {
              return NotFound();
          }
            return await _context.Rolet.ToListAsync();
        }

        // GET: api/Rolet/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rolet>> GetRolet(int id)
        {
          if (_context.Rolet == null)
          {
              return NotFound();
          }
            var rolet = await _context.Rolet.FindAsync(id);

            if (rolet == null)
            {
                return NotFound();
            }

            return rolet;
        }

        // PUT: api/Rolet/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRolet(int id, Rolet rolet)
        {
            if (id != rolet.Id)
            {
                return BadRequest();
            }

            _context.Entry(rolet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoletExists(id))
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

        // POST: api/Rolet
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rolet>> PostRolet(Rolet rolet)
        {
          if (_context.Rolet == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Rolet'  is null.");
          }
            _context.Rolet.Add(rolet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRolet", new { id = rolet.Id }, rolet);
        }

        // DELETE: api/Rolet/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRolet(int id)
        {
            if (_context.Rolet == null)
            {
                return NotFound();
            }
            var rolet = await _context.Rolet.FindAsync(id);
            if (rolet == null)
            {
                return NotFound();
            }

            _context.Rolet.Remove(rolet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoletExists(int id)
        {
            return (_context.Rolet?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
