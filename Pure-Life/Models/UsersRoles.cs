using Microsoft.AspNetCore.Identity;

namespace Pure_Life.Models
{
    public class UsersRoles
    {
        public IdentityUser User { get; set; }
        public IList<string> Roles { get; set; }
    }
}
