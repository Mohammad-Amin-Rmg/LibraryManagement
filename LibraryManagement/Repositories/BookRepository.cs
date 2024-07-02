using Data.Contexts;
using Data.Contracts;
using Data.Repositories.Models;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

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
            var book = _context.Books
                        .Where(x => x.Id == id)
                        .Select(x => new BookDto
                        {
                            Title = x.Title,
                            Description = x.Description,
                            Author = x.BookAuthors.Select(x => x.Author.Name).ToList(),
                            Category = x.BookCategories.Select(x => x.Category.Name).ToList(),
                            Publisher = x.Publisher,
                            BorrowsCount = x.Borrows.Select(x => x.Id).Count(),
                        }).ToList();

            var borrowBook = _context.Borrows.Where(x => x.ReturnedDate != null).Select(x => x.Book).Select(x => x.Title).ToList();

            return book;
        }

        public async Task<List<BookDto>> GetBooksAsync()
        {
            var getBook = _context.Books
                .Select(x => new BookDto
                {
                    Title = x.Title,
                    Authors = x.BookAuthors.Select(x => x.Author).ToList(),
                    Categories = x.BookCategories.Select(x => x.Category).ToList(),
                    Description = x.Description,
                    Publisher = x.Publisher
                }).ToList();

            return getBook;
        }

    }
}
