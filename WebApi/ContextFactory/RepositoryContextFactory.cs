using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repositories.EfCore;

namespace WebApi.ContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryDbContext>
    {
        public RepositoryDbContext CreateDbContext(string[] args)
        {
            //configurationBuilder
            var configuration = new ConfigurationBuilder().
                SetBasePath(Directory.GetCurrentDirectory()).
                AddJsonFile("appsettings.json").Build();
            //dbContextBuilder
            var builder = new DbContextOptionsBuilder<RepositoryDbContext>()
                .UseSqlServer(configuration.GetConnectionString("sqlConnection"),prj=>prj.MigrationsAssembly("WebApi"));

            return new RepositoryDbContext(builder.Options);
        }
    }
}
