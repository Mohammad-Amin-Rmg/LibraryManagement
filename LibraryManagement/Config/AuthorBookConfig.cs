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
    public class AuthorBookConfig : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            builder.HasKey(x => new { x.AuthorId, x.BookId });
            builder.HasOne(x => x.Author).WithMany(y => y.BookAuthors).HasForeignKey(x => x.AuthorId);
            builder.HasOne(x => x.Book).WithMany(y => y.BookAuthors).HasForeignKey(y => y.BookId);
        }
    }
}
