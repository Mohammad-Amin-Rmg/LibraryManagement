using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class BookAuthors
    {
        public Book Book { get; set; }
        public int BookId {  get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }

    }
}
