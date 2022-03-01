using System;
using System.Linq;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LibApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<Customer>>();

            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (!context.MembershipTypes.Any())
                    SeedMembershipTypes(context);

                if (!context.Roles.Any())
                    SeedRoles(context);

                if (!context.Customers.Any())
                    SeedCustomers(userManager);

                if (!context.Genre.Any())
                    SeedGenres(context);

                if (!context.Books.Any())
                    SeedBooks(context);

                context.SaveChanges();
            }
        }

        private static void SeedBooks(ApplicationDbContext context)
        {
            context.Books.AddRange(
                new Book
                {
                    GenreId = 6,
                    Name = "Reprieve",
                    AuthorName = "James Han Mattson",
                    ReleaseDate = DateTime.Parse("10/01/2012"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 10
                },
                new Book
                {
                    GenreId = 6,
                    Name = "A Cosmology of Monsters",
                    AuthorName = "Shaun Hamill",
                    ReleaseDate = DateTime.Parse("10/01/2012"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 10
                },
                new Book
                {
                    GenreId = 6,
                    Name = "The Haunting of Hill House",
                    AuthorName = "Shirley Jackson",
                    ReleaseDate = DateTime.Parse("10/01/2012"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 10
                },
                new Book
                {
                    GenreId = 6,
                    Name = "Mexican Gothic",
                    AuthorName = "Silvia Moreno-Garcia",
                    ReleaseDate = DateTime.Parse("10/01/2012"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 10
                },
                new Book
                {
                    GenreId = 1,
                    Name = "Alice’s Adventures in Wonderland",
                    AuthorName = "Lewis Carroll",
                    ReleaseDate = DateTime.Parse("10/01/2012"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 10
                },
                new Book
                {
                    GenreId = 4,
                    Name = "Gone Girl",
                    AuthorName = "Gillian Flynn",
                    ReleaseDate = DateTime.Parse("10/01/2012"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 10
                },
                new Book
                {
                    GenreId = 3,
                    Name = "The Blazing World",
                    AuthorName = " Margaret Cavendish",
                    ReleaseDate = DateTime.Parse("10/01/2012"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 10
                },
                new Book
                {
                    GenreId = 7,
                    Name = "Murder on the Orient Express",
                    AuthorName = "Agatha Christie",
                    ReleaseDate = DateTime.Parse("10/01/2012"),
                    DateAdded = DateTime.Now,
                    NumberInStock = 10
                }
            );
        }

        private static void SeedGenres(ApplicationDbContext context)
        {
            context.Genre.AddRange(
                new Genre
                {
                    Id = 1,
                    Name = "Fantasy"
                },
                new Genre
                {
                    Id = 2,
                    Name = "Romance"
                },
                new Genre
                {
                    Id = 3,
                    Name = "Sci-Fi"
                },
                new Genre
                {
                    Id = 4,
                    Name = "Criminal"
                },
                new Genre
                {
                    Id = 5,
                    Name = "Biography"
                },
                new Genre
                {
                    Id = 6,
                    Name = "Horror"
                },
                new Genre
                {
                    Id = 7,
                    Name = "Mystery"
                },
                new Genre
                {
                    Id = 8,
                    Name = "Thriller"
                }
            );
        }

        private static void SeedCustomers(UserManager<Customer> userManager)
        {
            var hasher = new PasswordHasher<Customer>();

            var customer1 = new Customer
            {
                Name = "Marcin Ziarko",
                Email = "marcin.ziarko@gmail.com",
                NormalizedEmail = "marcin.ziarko@gmail.com",
                UserName = "marcin.ziarko@gmail.com",
                NormalizedUserName = "marcin.ziarko@gmail.com",
                MembershipTypeId = 1,
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(null, "qwerty")
            };


            userManager.CreateAsync(customer1).Wait();
            userManager.AddToRoleAsync(customer1, "user").Wait();

            var customer2 = new Customer
            {
                Name = "Daenerys Targaryen",
                Email = "daenerys.targaryen@gmail.com",
                NormalizedEmail = "daenerys.targaryen@gmail.com",
                UserName = "daenerys.targaryen@gmail.com",
                NormalizedUserName = "daenerys.targaryen@gmail.com",
                MembershipTypeId = 1,
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(null, "test")
            };


            userManager.CreateAsync(customer2).Wait();
            userManager.AddToRoleAsync(customer2, "storemanager").Wait();

            var customer3 = new Customer
            {
                Name = "Cersei Lannister",
                Email = "cersei.lannister@gmail.com",
                NormalizedEmail = "cersei.lannister@gmail.com",
                UserName = "cersei.lannister@gmail.com",
                NormalizedUserName = "cersei.lannister@gmail.com",
                MembershipTypeId = 1,
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(null, "qwerty")
            };


            userManager.CreateAsync(customer3).Wait();
            userManager.AddToRoleAsync(customer3, "owner").Wait();
        }

        private static void SeedRoles(ApplicationDbContext context)
        {
            context.Roles.AddRange(
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User",
                    NormalizedName = "user"
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "StoreManager",
                    NormalizedName = "storemanager"
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Owner",
                    NormalizedName = "owner"
                }
            );

            context.SaveChanges();
        }

        private static void SeedMembershipTypes(ApplicationDbContext context)
        {
            context.MembershipTypes.AddRange(
                new MembershipType
                {
                    Id = 1,
                    Name = "Pay as You Go",
                    SignUpFee = 0,
                    DurationInMonths = 0,
                    DiscountRate = 0
                },
                new MembershipType
                {
                    Id = 2,
                    Name = "Monthly",
                    SignUpFee = 30,
                    DurationInMonths = 1,
                    DiscountRate = 10
                },
                new MembershipType
                {
                    Id = 3,
                    Name = "Quaterly",
                    SignUpFee = 90,
                    DurationInMonths = 3,
                    DiscountRate = 15
                },
                new MembershipType
                {
                    Id = 4,
                    Name = "Yearly",
                    SignUpFee = 300,
                    DurationInMonths = 12,
                    DiscountRate = 20
                }
            );

            context.SaveChanges();
        }
    }
}