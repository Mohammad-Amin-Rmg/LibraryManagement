using LibraryManagement.Models.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IcodeNext_Practice.Context
{
    public class DbContextDesignTimeFactory : IDesignTimeDbContextFactory<AppContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            string connectionString = @"Data Source=localhost; Initial Catalog=Amin_2;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
            var option = new DbContextOptionsBuilder<AppDbContext>().
                UseNpgsql(connectionString).
                Options;

            return new AppDbContext(option);
        }

        AppContext IDesignTimeDbContextFactory<AppContext>.CreateDbContext(string[] args)(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
