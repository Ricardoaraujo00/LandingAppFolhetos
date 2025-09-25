using LandingAppFolhetos.Shared;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LandingAppFolhetos.Data
{
    public class DatabaseDbContext : DbContext
    {
        public DatabaseDbContext(DbContextOptions<DatabaseDbContext> options) : base(options) { }

        public DbSet<Local> Local { get; set; }
        public DbSet<Nivel> Nivel { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString: "Filename=D:\\Users\\RicardoAraujo\\source\\repos\\InovarNasDecisoes\\Server\\inovardb.db",
                    sqliteOptionsAction: op =>
                    {
                        op.MigrationsAssembly(
                            Assembly.GetExecutingAssembly().FullName);
                    });
            optionsBuilder.EnableSensitiveDataLogging().LogTo(Console.WriteLine, LogLevel.Information);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
