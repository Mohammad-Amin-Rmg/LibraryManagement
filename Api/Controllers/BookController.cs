using Data.Contexts;
using Data.Contracts;
using Data.Repositories.Models;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFramework.Api;
using WebFramework.Filters;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiResultFilter]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookRepository _bookRepository;
    private readonly ApplicationDbContext _context;
    public BookController(IBookRepository bookRepository, ApplicationDbContext context)
    {
        _bookRepository = bookRepository;
        _context = context;
    }

    [HttpGet("{id:int}")]
    public async Task<List<BookDto>> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetDetailsBook(id);
        return books;
    }

    [HttpGet]
    public async Task<List<BookDto>> Get(CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetBooksAsync();
        return books;
    }

    [HttpPost]
    public async Task<ApiResult<Book>> Create(BookDto bookDto, CancellationToken cancellationToken)
    {
        var book = new Book
        {
            Title = bookDto.Title,
            Description = bookDto.Description,
            Publisher = bookDto.Publisher
        };
        await _bookRepository.Add(book, cancellationToken);

        foreach (var categoryId in bookDto.CategoryIds)
        {
            var bookCategory = new BookCategory
            {
                BookId = book.Id,
                CategoryId = categoryId
            };
            _context.BookCategories.Add(bookCategory);
        }
        await _context.SaveChangesAsync();


        return book;
    }

    [HttpGet("search")]
    public async Task<ApiResult<Book>> Search([FromQuery] string title, string author, string category, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.TableNoTracking
            .Where(x => x.Title.Contains(title))
            .Where(x => x.BookAuthors.Select(x => x.Author.Name).Contains(author))
            .Where(x => x.BookCategories.Select(x => x.Category.Name).Contains(category))
            .FirstOrDefaultAsync();

        if (book is null)
            return NotFound();
        return book;
    }

    [HttpDelete("{id:int}")]
    public async Task<ApiResult<List<string>>> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(cancellationToken, id);
        book.IsDeleted = true;

        await _context.SaveChangesAsync(cancellationToken);

        var undeletedBooks = _bookRepository.TableNoTracking
             .Where(x => x.IsDeleted == false)
             .Select(x => x.Title).ToList();

        return undeletedBooks;
    }

    [HttpPut]
    public async Task Update(int id, Book book, CancellationToken cancellationToken)
    {
        var bookUpdate = await _bookRepository.GetByIdAsync(cancellationToken, id);
        bookUpdate.Title = book.Title;
        bookUpdate.Description = book.Description;
        bookUpdate.Publisher = book.Publisher;
        bookUpdate.Categories = book.Categories;

        await _bookRepository.UpdateAsync(bookUpdate, cancellationToken);
    }
}

