using Entities;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class Language: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    
    }
}


//protected override void OnModelCreating(ModelBuilder modelBuilder)
//{
//    modelBuilder.Entity<Language>()
//        .HasOne(l => l.Book)
//        .WithOne(b => b.Language)
//        .HasForeignKey<Book>(b => b.LanguageId);
//}
