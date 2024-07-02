using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber {get; set; }
        public virtual ICollection<Borrow> Borrows { get; set; }
    }
}
