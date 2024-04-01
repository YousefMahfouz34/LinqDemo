
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Linqe.Context
{
    public class LinqeContext : DbContext
    {
        public LinqeContext()
        {
            
        }
        public LinqeContext(DbContextOptions<LinqeContext> options):base(options) { }
       public DbSet<Empolyee> Empolyee { get; set;}
        public DbSet<Department> Department { get; set;}
        protected override void OnConfiguring(DbContextOptionsBuilder
            optionsBuilder) =>
             optionsBuilder.UseSqlServer
             (@"Server=.;Database=linqdb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    }
}
