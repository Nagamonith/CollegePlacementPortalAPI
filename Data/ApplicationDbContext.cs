using CollegePlacementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CollegePlacementAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<PlacementOfficer> PlacementOfficers { get; set; }
    }
}
