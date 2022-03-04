using System;
using System.Linq;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibApp.Models
{
    public static class SeedData
    {
       
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.MembershipTypes.Any())
                {
                    Console.WriteLine("Database already seeded");
                    return;
                }

                if (!context.MembershipTypes.Any())
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
                          });
               

                    context.Customers.AddRange(
                    new Customer
                    {

                        Name = "Jakub Janiec",
                        Birthdate = new DateTime(1998, 05, 25),
                        HasNewsletterSubscribed = false,
                        MembershipTypeId = 1
                    },
                    new Customer
                    {

                        Name = "Korwin Mikke",
                        Birthdate = new DateTime(1998, 05, 25),
                        HasNewsletterSubscribed = false,
                        MembershipTypeId = 3
                    },
                    new Customer
                    {

                        Name = "Vladimir Putin",
                        Birthdate = new DateTime(1998, 05, 25),
                        HasNewsletterSubscribed = false,
                        MembershipTypeId = 2
                    });
                    context.SaveChanges();
                

            
                    context.Books.AddRange(
                new Book
                {
                    Name = "Mama Darka"
                },
                new Book
                {
                    Name = "Champions"
                },
                new Book
                {
                    Name = "Mine Craft"
                }
                );
                    context.SaveChanges();
                }

                context.SaveChanges();
            }
        }
    }
}