using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalesOnTransport.Back.Models
{
    public class Scan
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
    }
}
