using Data.Contexts;
using Data.Contracts;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class BorrowRepository : Repository<Borrow>, IBorrowRepository
    {
        private readonly ApplicationDbContext _context;
        public BorrowRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
    }
}
