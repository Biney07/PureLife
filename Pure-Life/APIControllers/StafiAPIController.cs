using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pure_Life.Data;
using Pure_Life.Models;
using Pure_Life.ViewModel;
using Pure_Life.ViewModel.Stafi;
using Pure_Life.ViewModels;

namespace Pure_Life.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StafiAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public StafiAPIController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
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


		
		[HttpGet("GetStafiByLemi/{id}")]
		public async Task<IActionResult> GetStafiByLemi(int id)
		{
			if (_context.Stafi == null)
			{
				return NotFound();
			}
			var stafi = await _context.Stafi.Where(x=>x.LemiaId == id).ToListAsync();    

			if (stafi == null)
			{
				return NotFound();
			}

			return Ok(stafi);
		}

		// PUT: api/StafiAPI/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
        public async Task<IActionResult> PutStafi(int id, [FromBody] EditStafiApiViewModel request)
        {
            var stafi = await _context.Stafi.FindAsync(id);
            if (stafi == null)
            {
                return NotFound();
            }

            // Update the specific fields
            stafi.Emri = request.Emri;
            stafi.Mbiemri = request.Mbiemri;
            stafi.NrTel = request.NrTel;
            stafi.Email = request.Email;

            // Add the other fields that need to be updated
            stafi.NrLeternjoftimit = request.NrLeternjoftimit;
            stafi.Gjinia = request.Gjinia;
            stafi.DataLindjes = request.DataLindjes;
            stafi.NrLincences = request.NrLincences;
            stafi.PictureUrl = stafi.PictureUrl;
            stafi.ShtetiId = request.ShtetiId;
            stafi.Qyteti = request.Qyteti;
            stafi.NacionalitetiId = request.NacionalitetiId;
            stafi.ModifiedDate = request.ModifiedDate;
            stafi.ModifiedFrom = request.ModifiedFrom;

            _context.Entry(stafi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // Retrieve the updated user from the database
                var updatedStafi = await _context.Stafi.FindAsync(id);
                var token = CreateToken(updatedStafi);
                var userData = new
                {
                    Id = updatedStafi.Id,
                    NrLeternjoftimit = updatedStafi.NrLeternjoftimit,
                    Emri = updatedStafi.Emri,
                    Mbiemri = updatedStafi.Mbiemri,
                    Gjinia = updatedStafi.Gjinia,
                    DataLindjes = updatedStafi.DataLindjes,
                    NrLincences = updatedStafi.NrLincences,
                    NrTel = updatedStafi.NrTel,
                    PictureUrl = updatedStafi.PictureUrl,
                    RoletId = updatedStafi.RoletId,
                    ShtetiId = updatedStafi.ShtetiId,
                    Qyteti = updatedStafi.Qyteti,
                    NacionalitetiId = updatedStafi.NacionalitetiId,
                    LemiaId = updatedStafi.LemiaId,
                    Email = updatedStafi.Email,
                    EmailZyrtar = updatedStafi.EmailZyrtar,
                    token = token.ToString(),
                };

                // Return the new object with the updated user and token
                return Ok(userData);
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


        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<Stafi>>> GetAllStafi()
        {
            var stafiList = await _context.Stafi.ToListAsync();
            if (stafiList == null || !stafiList.Any())
            {
                return NotFound("No Stafi found.");

            }
            return stafiList;
        }

        [HttpPost("login")]
        public async Task<ActionResult<Object>> GetStafiByEmailAndPassword([FromBody] LoginViewModel request)
        {
            if (string.IsNullOrEmpty(request.EmailAddress) || string.IsNullOrEmpty(request.Password))
            {
                return Problem("Email or password is null.");
            }

            var user = await _context.Stafi.FirstOrDefaultAsync(x => x.EmailZyrtar == request.EmailAddress);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Directly compare the password from the user object and the request
            if (user.Password != request.Password)
            {
                return NotFound("Incorrect password.");
            }

            // Create a new object with only the properties we want to return
            var token = CreateToken(user);
            var userData = new
            {
                Id = user.Id,
                NrLeternjoftimit = user.NrLeternjoftimit,
                Emri = user.Emri,
                Mbiemri = user.Mbiemri,
                Gjinia = user.Gjinia,
                DataLindjes = user.DataLindjes,
                NrLincences = user.NrLincences,
                NrTel = user.NrTel,
                PictureUrl = user.PictureUrl,
                RoletId = user.RoletId,
                ShtetiId = user.ShtetiId,
                Qyteti = user.Qyteti,
                NacionalitetiId = user.NacionalitetiId,
                LemiaId = user.LemiaId,
                Email = user.Email,
                EmailZyrtar = user.EmailZyrtar,
                token = token.ToString(),
            };

            // Return the new object
            return userData;
        }


        private string CreateToken(Stafi stafi)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, stafi.Emri),
                new Claim(ClaimTypes.Sid, stafi.Id.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        [HttpGet("me")]
        public ActionResult<Stafi> GetCurrentUser()
        {
            // Check if the token has expired
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token is missing.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value)),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            try
            {
                // Validate the token
                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                var expiryClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Expiration)?.Value;
                if (expiryClaim != null && DateTime.TryParse(expiryClaim, out var expiryTime))
                {
                    if (expiryTime <= DateTime.UtcNow)
                    {
                        return Unauthorized("Token has expired.");
                    }
                }
            }
            catch (Exception)
            {
                return Unauthorized("Invalid token.");
            }

            // Return the new object
            return Ok("Token is valid");
        }




        // DELETE: api/StafiAPI/5
        [HttpDelete("deleteStafi")]
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
        [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);

                if (passwordCheck)
                {
                    // Authentication successful
                    var role = await _userManager.GetRolesAsync(user);
                    var response = new
                    {
                        Success = true,
                        Role = role.FirstOrDefault(),
                        Message = "Authentication successful."
                    };

                    return Ok(response);
                }
            }

            // Invalid email or password
            var errorResponse = new
            {
                Success = false,
                Message = "Invalid email or password."
            };

            return Unauthorized(errorResponse);
        }

        [HttpPost]
        [Route("api/logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Logged out successfully.");
        }





        private bool StafiExists(int id)
        {
            return (_context.Stafi?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
