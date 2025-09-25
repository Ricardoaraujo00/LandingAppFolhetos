using LandingAppFolhetos.Shared;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LandingAppFolhetos.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Local> Locais { get; set; } = default!;
        public DbSet<Nivel> Niveis { get; set; } = default!;
    }
}
