using alamos_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace alamos_Backend.Data
{
    public class AplicationDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AplicationDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var strConnection = Configuration.GetConnectionString("DefaultConnection");
            options.UseMySql(strConnection, ServerVersion.AutoDetect(strConnection));
        }

        public DbSet<Users> users { get; set; }


    }
}
