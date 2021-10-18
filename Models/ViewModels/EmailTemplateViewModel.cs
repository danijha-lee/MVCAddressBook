using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCAddressBook.Models.ViewModels
{
    public class EmailTemplateViewModel
    {
        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }

        public SelectList CategoryList { get; set; }
    }
}