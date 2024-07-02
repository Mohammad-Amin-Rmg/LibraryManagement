using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models.Config
{
    public class AuthorBookConfig : IEntityTypeConfiguration<BookAuthors>
    {
        public void Configure(EntityTypeBuilder<BookAuthors> builder)
        {
            builder.HasOne(x=>x.Author).WithMany(y=>y.BookAuthors).HasForeignKey(x=>x.AuthorId);
        }
    }
}
