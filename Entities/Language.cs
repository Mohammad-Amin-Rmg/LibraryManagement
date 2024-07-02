using Entities;
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
        public string Title { get; set; }
        public Book Book { get; set; }
    }
}
