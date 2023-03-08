using Microsoft.EntityFrameworkCore;
using Profissional.Models.Domain;

namespace Profissional.Data
{
    public class ProfissionalDbContext : DbContext
    {
        public ProfissionalDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
