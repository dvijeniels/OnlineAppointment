using Microsoft.EntityFrameworkCore;

namespace Zapis.Models.Data
{
    public class zapisContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public zapisContext(DbContextOptions<zapisContext> options) : base(options) { }
        //public zapisContext(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    // connect to sql server with connection string from app settings
        //    options.UseSqlServer(Configuration.GetConnectionString("ZapisString"));
        //}

        public virtual DbSet<appointment> appointment { get; set; }
        public virtual DbSet<category> category { get; set; }
        public virtual DbSet<report> report { get; set; }
    }
}

