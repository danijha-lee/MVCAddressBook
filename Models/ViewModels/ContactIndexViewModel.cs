using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCAddressBook.Models.ViewModels
{
    public class ContactIndexViewModel
    {
        public IEnumerable<Contact> Contacts { get; set; }
        public SelectList CategoryFilter { get; set; }
    }
}