using Microsoft.EntityFrameworkCore;
using WareHouse_MarioOrlando.Models.Domain;

namespace WareHouse_MarioOrlando.Data
{
    public class MVCDbContext : DbContext
    {
        public MVCDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet <Category> Categories { get; set; }
    }
}
