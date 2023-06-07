using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pure_Life.Data;
using Pure_Life.Models;
using Pure_Life.Services;
using Pure_Life.ViewModel.Termini;
using Pure_Life.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Pure_Life.APIControllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TerminiAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private ICurrentUser _currentUser;
 



        public TerminiAPIController(ApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
      
        }

       
        [HttpPost]
        public async Task<IActionResult> Create(int stafiId)
        {
            /*
             * 
             8:00 - 8:30
             8:30 - 9:00
             9:00 - 9:30
             9:30 - 10:00
             10:30 - 11:00
             11:00 - 11:30
            11:30 - 12:00

            13:00 - 13:30
            13:30 - 14:00
            14:00 - 14:30
            14:30 - 15:00
            15:30 - 16:00

             */
       TimeSpan[] timeSlots = new TimeSpan[]
      {
        new TimeSpan(8, 0, 0),   // 8:00 - 8:30
        new TimeSpan(8, 30, 0),  // 8:30 - 9:00
        new TimeSpan(9, 0, 0),   // 9:00 - 9:30
        new TimeSpan(9, 30, 0),  // 9:30 - 10:00
        new TimeSpan(10, 30, 0), // 10:30 - 11:00
        new TimeSpan(11, 0, 0),  // 11:00 - 11:30
        new TimeSpan(11, 30, 0), // 11:30 - 12:00
        new TimeSpan(13, 0, 0),  // 13:00 - 13:30
        new TimeSpan(13, 30, 0), // 13:30 - 14:00
        new TimeSpan(14, 0, 0),  // 14:00 - 14:30
        new TimeSpan(14, 30, 0), // 14:30 - 15:00
        new TimeSpan(15, 30, 0)  // 15:30 - 16:00
   };
            //var user = _currentUser.GetCurrentUserName();
            //var stafi = _context.Stafi.Where(x => x.Emri == user).FirstOrDefault();
            var stafi = _context.Stafi.Where(x => x.Id == stafiId).FirstOrDefault();

          /*  if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }*/
            DateTime currentDateTime = DateTime.Now;
            var terminiList = new List<Termini>();

            foreach (var timeSlot in timeSlots)
            {

                var termini = new Termini()
                {
                    StartTime = (currentDateTime.Date + timeSlot).ToString(),
                    EndTime = (currentDateTime.Date + timeSlot.Add(new TimeSpan(0, 30, 0))).ToString(),
                    Status = false,
                    Price = 0,
                    StafiId = stafi.Id,
                    PacientiId = null,
                    InsertedDate = currentDateTime,
                    InsertedFrom = _currentUser.GetCurrentUserName(),
                };
                terminiList.Add(termini);
            }
            await _context.AddRangeAsync(terminiList);
            await _context.SaveChangesAsync();
            return Ok();

        }

        

        [HttpGet]
        public IActionResult Index()
        {
            var terminet = _context.Terminet.ToList();
            return Ok(terminet);
        }
    }
}
