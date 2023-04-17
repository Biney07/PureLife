using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pure_Life.Data;
using Pure_Life.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pure_Life.Controllers
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
        // GET: api/<StaffController>
        [HttpGet]
        public ActionResult<IEnumerable<Shteti>> Get()
        {
            return _context.Shteti.ToList();
        }
    }
}
