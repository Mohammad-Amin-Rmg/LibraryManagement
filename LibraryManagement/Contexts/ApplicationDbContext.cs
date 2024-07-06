using Data.Config;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Borrow> Borrows { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Language> Languages { get; set; }

        //string connectionString = "Host=localhost; Database=postgres; Username=postgres; Password=147852";


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MemberConfig).Assembly);
            //modelBuilder.Entity<Book>().HasOne(x => x.Language).WithOne(x => x.);
            //modelBuilder.Entity<Book>().HasNoKey();
        }
    }
}
