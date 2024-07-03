using Data.Contexts;
using Data.Contracts;
using LibraryManagement.Models;

namespace Data.Repositories
{
    public class LanguageRepository : Repository<Language>, ILanguageRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public LanguageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        

    }
}
