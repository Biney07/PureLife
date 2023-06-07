using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Pure_Life.Data;
using Pure_Life.Models;
using Pure_Life.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pure_Life.APIControllers
{
    public class AccountAPIController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountAPIController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }
            

            // Perform authentication logic (e.g., validate credentials against the database)
            var isAuthenticated = await PerformAuthentication(loginVM.EmailAddress, loginVM.Password);

            if (isAuthenticated)
            {
                var token = GenerateToken(loginVM.EmailAddress);
                var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);

                return Ok(new
                {
                    Token = token,
                    EmailAddress = user.Email,
                    Username = user.UserName
                });
            }

            return Unauthorized();
        }

        private async Task<bool> PerformAuthentication(string email, string password)
        {
            var user =  await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }

            /*   var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);*/
            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
            return result.Succeeded;
        }

        private string GenerateToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:Token"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Email, email)
        }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpPost]
        [Route("api/logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Logged out successfully.");
        }

    }
}
