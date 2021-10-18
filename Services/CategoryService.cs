using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCAddressBook.Data;
using MVCAddressBook.Models;
using MVCAddressBook.Services.Interfaces;

namespace MVCAddressBook.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddContactToCategoryAsync(int categoryId, int contactId)
        {
            var contact = await _context.Contacts.FindAsync(contactId);
            var category = await _context.Categories.FindAsync(categoryId);
            category.Contacts.Add(contact);

            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Category>> GetContactCategoriesAsync(int contactId)
        {
            var contact = await _context.Contacts.Include(c => c.Categories).FirstOrDefaultAsync(c => c.Id == contactId);
            return contact.Categories;
        }

        public async Task<int> GetContactCountForCategoryAsync(int categoryId)
        {
            var category = await _context.Categories.Include(c => c.Contacts).FirstOrDefaultAsync(c => c.Id == categoryId);
            return category.Contacts.Count;
        }

        public async Task RemoveContactFromCategoryAsync(int categoryId, int contactId)
        {
            var contact = await _context.Contacts.FindAsync(contactId);
            var category = await _context.Categories.FindAsync(categoryId);
            category.Contacts.Remove(contact);

            await _context.SaveChangesAsync();
        }
    }
}