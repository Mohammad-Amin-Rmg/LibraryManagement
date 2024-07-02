using Data.Contexts;
using LibraryManagement.Migrations;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Text.Json;
static void Main() { }
//using var db = new ApplicationDbContext();


//Console.WriteLine("Authors With Books\n");

//var authorsWithBooks = await db.Authors.
//                        Select(x => new
//                        {
//                            x.Name,
//                            CountOfBook = x.BookAuthors.Select(x => x.Book).Count()
//                        }).ToArrayAsync();

//Console.WriteLine(JsonSerializer.Serialize(authorsWithBooks));


//Console.WriteLine("\n----------------------------------------------------------------------");

//Console.WriteLine("Categories With Books\n");


//var categoriesWithBooks = await db.Categories.
//                        Select(x => new
//                        {
//                            Category = x.Name,
//                            Book = x.BookCategories.Select(x => x.Book).Select(x => x.Title)
//                        }).ToArrayAsync();


//Console.WriteLine(JsonSerializer.Serialize(categoriesWithBooks));

//Console.WriteLine("\n----------------------------------------------------------------------");

//Console.WriteLine("books Have Borrowed\n");

//var booksHaveBorrowed = await db.Borrows.
//                      Select(x => new
//                      {
//                          BorrowedBooks = x.Book.Title
//                      }).ToArrayAsync();

//Console.WriteLine(JsonSerializer.Serialize(booksHaveBorrowed));


//Console.WriteLine("\n----------------------------------------------------------------------");



//Console.WriteLine("books with authors and categories\n");

//var booksWithAuthorsCategories = await db.Books.
//                                Select(x => new
//                                {
//                                    Books = x.Title,
//                                    Authors = x.BookAuthors.Select(x => x.Author.Name),
//                                    Categories = x.BookCategories.Select(x => x.Category.Name)
//                                }).ToArrayAsync();

//Console.WriteLine(JsonSerializer.Serialize(booksWithAuthorsCategories));


//Console.WriteLine("\n----------------------------------------------------------------------");


//var memebersCount=await db.Members.CountAsync();

//Console.WriteLine($"Memebers Count is {memebersCount}");

// books with authors and categories

// authors with books count

// categories with books

// memebers count

// books have borrow