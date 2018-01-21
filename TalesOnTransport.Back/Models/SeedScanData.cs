using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace TalesOnTransport.Back.Models
{
    public static class SeedScanData
    {
        public static void Initialise(IServiceProvider serviceProvider)
        {
            using (var context = new ScanContext(
    serviceProvider.GetRequiredService<DbContextOptions<ScanContext>>()))
            {
                if (context.Scan.Any())
                {
                    return;
                }

                context.Scan.AddRange(
                    new Scan
                    {
                        Id = new Guid("246744ce-f93f-47c8-9b6f-7d17ef597762"),
                        BookId = new Guid("6811b546-a97c-4c71-9166-70b7e8e13341") // First book
                    },
                    new Scan
                    {
                        Id = new Guid("bcc7b933-fde0-41fc-88d1-94bccda5896a"),
                        BookId = new Guid("b210efc1-bd90-43dd-b333-c566d7da170a") // Second book
                    },
                    new Scan
                    {
                        Id = new Guid("2a75e809-7f8c-4acb-8b95-9b43b2cc066e"),
                        BookId = new Guid("6811b546-a97c-4c71-9166-70b7e8e13341") // First book
                    },
                    new Scan
                    {
                        Id = new Guid("5d7b6c04-3074-401e-8000-b468cdc7e6d8"),
                        BookId = new Guid("ffc6ba8e-fac7-4869-b614-72482ac53710") // Third book
                    }

                );
                context.SaveChanges();
            }

        }
    }
}
