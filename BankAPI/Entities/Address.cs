using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Entities
{
    public class Address
    {
        public int Id { get; set; }

        [StringLength(256)]
        public string Country { get; set; }

        [StringLength(256)]
        public string County { get; set; }

        [StringLength(256)]
        public string City { get; set; }

        [StringLength(256)]
        public string Street { get; set; }

        [StringLength(256)]
        public string Number { get; set; }

        [StringLength(256)]
        public string Block { get; set; }

        [StringLength(256)]
        public string Stairway { get; set; }

        [StringLength(256)]
        public string Apartment { get; set; }

    }
}
