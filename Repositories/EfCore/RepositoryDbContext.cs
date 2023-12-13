using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.EfCore.Config;


namespace Repositories.EfCore
{
    public class RepositoryDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public RepositoryDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfig());
        }
    }
}
