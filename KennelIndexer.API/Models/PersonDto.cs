using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KennelIndexer.API.Models
{
    public class PersonDto
    {
        public Guid PersonId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ReasonsForBeingOnTheList { get; set; }
    }
}
