using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Data.Entities
{
    public class SeedDB
    {
        private readonly DataContext context;
        private readonly UserManager<User> userManager;
        private Random random;

        public SeedDB(DataContext context,UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.random = new Random();
        }
        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            var user = await this.userManager.FindByEmailAsync("efraju.56@gmail.com");
            if (user==null)
            {
                user = new User
                {
                    FirstName = "Efra",
                    LastName = "Julca",
                    Email = "efraju.56@gmail.com",
                    UserName = "efraju.56@gmail.com",
                    PhoneNumber="984231351"
                };

                var result = await this.userManager.CreateAsync(user, "123456");
                if (result!=IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }

            if (!this.context.Products.Any())
            {
                this.AddProduct("Agua Cielo",user);
                this.AddProduct("Celular", user);
                this.AddProduct("Laptop Lenovo", user);
                await this.context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name,User user)
        {
            this.context.Products.Add(new Product
            {
                Name = name,
                Price = this.random.Next(1000),
                IsAvailabe = true,
                Stock = this.random.Next(100),
                User=user
            });
        }
    }
}
