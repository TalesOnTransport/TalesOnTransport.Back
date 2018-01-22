using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalesOnTransport.Back.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public int PIN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int TimesScanned { get; set; }
    }
}
