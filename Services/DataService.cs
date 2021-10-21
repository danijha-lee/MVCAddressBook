using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVCAddressBook.Data;
using MVCAddressBook.Models;
using MVCAddressBook.Services.Interfaces;

namespace MVCAddressBook.Services
{
    //Responsible for making sure there is at least one user and some data in the database
    public static class DataService
    {
        public static async Task ManageDataAsync(IHost host)
        {
            using var svcScope = host.Services.CreateScope();
            var svcProvider = svcScope.ServiceProvider;

            //Database Access
            var dbContextSvc = svcProvider.GetRequiredService<ApplicationDbContext>();
            //UserManager to create a default user
            var userManagerSvc = svcProvider.GetRequiredService<UserManager<AppUser>>();

            //Category Service to add new Contacts to Categories
            var categorySvc = svcProvider.GetRequiredService<ICategoryService>();
            //For published applications - this runs update-database onproduction code
            await dbContextSvc.Database.MigrateAsync();

            //Custom Address Book seeding methods
            await SeedDefaultUserAsync(userManagerSvc);
            await SeedDefaultContacts(dbContextSvc);
            await SeedDefaultCategoriesAsync(dbContextSvc);
            await SeedDefaultCategoryAssign(categorySvc);
        }

        private static Task SeedDefaultCategoryAssign(ICategoryService categorySvc)
        {
            throw new NotImplementedException();
        }

        private static Task SeedDefaultCategoriesAsync(ApplicationDbContext dbContextSvc)
        {
            throw new NotImplementedException();
        }

        private async static Task SeedDefaultContacts(ApplicationDbContext dbContextSvc)
        {
            var userId = dbContextSvc.Users.FirstOrDefault(u => u.Email == "DemoUser@mailinator.com").Id;
            var defaultContact = new Contact
            {
                UserId = userId,
                Created = DateTime.Now,
                FirstName = "Henry",
                LastName = "McCoy",
                Address1 = "1407 Graymalkin Ln.",
                City = "Salem Center",
                Phone = "555-555-5544",
                State = Enums.States.NY,
                Email = "hankmccoy@starktower.com"
            };

            try
            {
                var contact = await dbContextSvc.Contacts.AnyAsync(c => c.Email == "hankmccoy@starktower.com" && c.UserId == userId);

                if (!contact)
                {
                    await dbContextSvc.AddAsync(defaultContact);
                    await dbContextSvc.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("**** ERROR ***** ");
                Console.WriteLine("Error Seeding Default User");
                Console.WriteLine(ex.Message);
                Console.WriteLine("**** END OF ERROR ******");
            }
        }

        private async static Task SeedDefaultUserAsync(UserManager<AppUser> userManagerSvc)
        {
            var defaultUser = new AppUser
            {
                UserName = "DemoUser@mailinator.com",
                Email = "DemoUser@mailinator.com",
                FirstName = "Demo",
                LastName = "User",
                EmailConfirmed = true,
            };
            try
            {
                var user = await userManagerSvc.FindByNameAsync(defaultUser.Email);

                if (user is null)
                {
                    await userManagerSvc.CreateAsync(defaultUser, "Abc&123!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("**** ERROR ***** ");
                Console.WriteLine("Error Seeding Default User");
                Console.WriteLine(ex.Message);
                Console.WriteLine("**** END OF ERROR ******");
            }
        }
    }
}