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
        public string Name { get; set; }
    }
}
