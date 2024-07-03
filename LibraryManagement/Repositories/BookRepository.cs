using Data.Contexts;
using Data.Contracts;
using Data.Repositories.Models;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Data.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<BookDto>> GetDetailsBook(int id)
        {
            var bookSatae = _context.Books.Select(x => x.Borrows.Select(x => x.ReturnedDate));
            var book = _context.Books
                        .Where(x => x.Id == id)
                        .Select(x => new BookDto
                        {
                            Title = x.Title,
                            Description = x.Description,
                            IsBorrowed = x.Borrows.Any(x => x.ReturnedDate == null),
                            Author = x.BookAuthors.Select(x => x.Author.Name).ToList(),
                            Categories = x.BookCategories.Select(x => x.Category.Name).ToList(),
                            Publisher = x.Publisher,
                            BorrowsCount = x.Borrows.Select(x => x.Id).Count(),
                        }).ToList();


            return book;
        }

        public async Task<List<BookDto>> GetBooksAsync()
        {
            var getBook = _context.Books
                .Select(x => new BookDto
                {
                    Title = x.Title,
                    Authors = x.BookAuthors.Select(x => x.Author).ToList(),
                    Categories = x.BookCategories.Select(x => x.Category.Name).ToList(),
                    Description = x.Description,
                    Publisher = x.Publisher
                }).ToList();

            return getBook;
        }

    }
}
