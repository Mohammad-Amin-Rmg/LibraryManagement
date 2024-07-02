using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Models
{
    public class BookDto
    {
        public required string Title { get; set; }
        public List<Author>? Authors { get; set; }
        public List<string>? Author { get; set; }
        public List<Category>? Categories { get; set; }
        public List<string>? Category { get; set; }
        public int BorrowsCount { get; set; }
        public bool IsBorrowed { get; set; }
        public string? Description { get; set; }
        public string? Publisher { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
