using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class BookCategories
    {
        public Category Category { get; set; }
        public int CategoryId {  get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
    }
}
