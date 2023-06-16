using Microsoft.EntityFrameworkCore;
using VanisBookWeb.Models;

namespace Vanis.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

    }
}
