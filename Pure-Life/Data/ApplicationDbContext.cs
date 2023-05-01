using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pure_Life.Models;

namespace Pure_Life.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    /*    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }*/
        public DbSet<Stafi> Stafi { get; set; }
        public DbSet<Nacionaliteti> Nacionaliteti { get; set; }
        public DbSet<Shteti> Shteti { get; set; }
        public DbSet<Lemia> Lemia { get; set; }
        public DbSet<Rolet> Rolet { get; set; }
        public DbSet<Kujdestarite> Kujdestarite { get; set; }
        public DbSet<DitetEPushimeve> DitetEPushimeve { get; set; }

	}
}
