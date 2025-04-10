using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGrowthPath.Infrastructure.Identity
{
    public class ApplicationIdentityDbContextFactory : IDesignTimeDbContextFactory<ApplicationIdentityDbContext>
    {
        public ApplicationIdentityDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ProjectGrowthPath.UserInterface"))
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = config.GetConnectionString("IdentityConnections");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationIdentityDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new ApplicationIdentityDbContext(optionsBuilder.Options);
        }
    }
}