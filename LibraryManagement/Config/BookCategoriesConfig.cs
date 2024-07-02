using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Config
{
    public class BookCategoriesConfig : IEntityTypeConfiguration<BookCategory>
    {

        public void Configure(EntityTypeBuilder<BookCategory> builder)
        {
            builder.HasKey(x => new { x.CategoryId, x.BookId });
            builder.HasOne(x => x.Book).WithMany(y => y.BookCategories).HasForeignKey(x => x.BookId);
            builder.HasOne(x => x.Category).WithMany(y => y.BookCategories).HasForeignKey(x => x.CategoryId);
        }
    }
}
