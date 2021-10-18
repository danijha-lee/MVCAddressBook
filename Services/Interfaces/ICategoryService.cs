using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCAddressBook.Models;

namespace MVCAddressBook.Services.Interfaces
{
    public interface ICategoryService
    {
        Task AddContactToCategoryAsync(int categoryId, int contactId);

        Task RemoveContactFromCategoryAsync(int categoryId, int contactId);

        Task<ICollection<Category>> GetContactCategoriesAsync(int contactId);

        Task<int> GetContactCountForCategoryAsync(int categoryId);
    }
}