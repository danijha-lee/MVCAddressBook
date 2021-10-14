using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAddressBook.Models
{
    public class Category
    {
        //Primary Key
        public int Id { get; set; }

        //Foreign Key
        public string UserId { get; set; }

        public string Name { get; set; }

        public virtual AppUser User { get; set; }

        public ICollection<Contact> Contacts { get; set; } = new HashSet<Contact>();
    }
}