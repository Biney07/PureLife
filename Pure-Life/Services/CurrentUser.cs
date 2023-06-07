using Microsoft.AspNetCore.Identity;
using Pure_Life.Models;
using System.Security.Claims;

namespace Pure_Life.Services
{
    public class CurrentUser : ICurrentUser
    {
        /*  private readonly IHttpContextAccessor _httpContextAccessor;

          public CurrentUser(IHttpContextAccessor httpContextAccessor)
          {
              _httpContextAccessor = httpContextAccessor;
          }

          public string GetCurrentUserName()
          {
              var fullName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);

              if (!string.IsNullOrEmpty(fullName))
              {
                  return fullName;
              }
              return _httpContextAccessor.HttpContext.User.Identity.Name;
          }*/
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUserName()
        {
            var email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            var user = _userManager.FindByEmailAsync(email).Result;
            return user?.FullName;
        }
      
    }
}
