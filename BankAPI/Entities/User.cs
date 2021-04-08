using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Username { get; set; }

        [Required]
        [StringLength(256)]
        public string PasswordToken { get; set; }

        [EmailAddress]
        [Required]
        [StringLength(256)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(256)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(256)]
        public string LastName { get; set; }

        [Required]
        [StringLength(256)]
        public string CNP { get; set; }

        [Required]
        [StringLength(256)]
        public string CI { get; set; }
        
    }
}
