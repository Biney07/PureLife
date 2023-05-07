using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pure_Life.Models;
using Pure_Life.Models.Analiza;

namespace Pure_Life.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
  
        public DbSet<Stafi> Stafi { get; set; }
        public DbSet<Nacionaliteti> Nacionaliteti { get; set; }
        public DbSet<Shteti> Shteti { get; set; }
        public DbSet<Lemia> Lemia { get; set; }
        public DbSet<Rolet> Rolet { get; set; }
        public DbSet<Kujdestarite> Kujdestarite { get; set; }
        public DbSet<DitetEPushimeve> DitetEPushimeve { get; set; }
		public DbSet<Sherbimet> Sherbimet { get; set; }
        public DbSet<Analiza> Analizat { get; set; }
        public DbSet<AnalizaLloji> AnalizatLlojet { get; set; }
        public DbSet<Lloji> Llojet { get; set; }


    }
}
