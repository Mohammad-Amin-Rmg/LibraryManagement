using Data.Repositories.Models;
using LibraryManagement.Models;

namespace Data.Contracts
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<List<BookDto>> GetBooksAsync();
        Task<List<BookDto>> GetDetailsBook(int id);
    }
}