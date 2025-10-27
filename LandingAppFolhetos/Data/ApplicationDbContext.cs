using LandingAppFolhetos.Shared;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LandingAppFolhetos.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Local> Locais { get; set; } = default!;
        public DbSet<Nivel> Niveis { get; set; } = default!;
        public DbSet<RegistoLeituraFolheto> RegistoLeituraFolhetos  { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RegistoLeituraFolheto>()
                .HasOne<ApplicationUser>() // sem propriedade de navegação
                .WithMany()
                .HasForeignKey(r => r.IdUtilizador)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
