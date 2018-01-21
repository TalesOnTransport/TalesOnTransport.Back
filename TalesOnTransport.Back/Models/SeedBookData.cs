using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace TalesOnTransport.Back.Models
{
    public static class SeedBookData
    {
        public static void Initialise(IServiceProvider serviceProvider)
        {
            using (var context = new BookContext(
    serviceProvider.GetRequiredService<DbContextOptions<BookContext>>()))
            {
                if (context.Book.Any())
                {
                    return;
                }

                context.Book.AddRange(
                    new Book
                    {
                        Id = new Guid("6811b546-a97c-4c71-9166-70b7e8e13341"),
                        Title = "Ender's Game",
                        Author = "Orson Scott Card",
                        TimesScanned = 2
                    },
                    new Book
                    {
                        Id = new Guid("b210efc1-bd90-43dd-b333-c566d7da170a"),
                        Title = "The Three Body Problem",
                        Author = "Liu Cixin",
                        TimesScanned = 1
                    },
                    new Book
                    {
                        Id = new Guid("ffc6ba8e-fac7-4869-b614-72482ac53710"),
                        Title = "Snow Crash",
                        Author = "Neal Stephenson",
                        TimesScanned = 1
                    }
                );
                context.SaveChanges();
            }

        }
    }
}
