using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class Book
    {
        public int BookId {  get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Publisher {  get; set; }

        public virtual ICollection<BookAuthors> BookAuthors { get; set; }
        public virtual ICollection<BookCategories> BookCategories { get; set; } 
        public virtual ICollection<Borrow> Borrows { get; set; }

    }
}
