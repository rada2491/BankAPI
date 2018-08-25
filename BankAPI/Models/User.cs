using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Models
{
    public class User
    {

        public User()
        {
            Accounts = new List<Account>();
        }

        /*[Required]
        [UserType]
        public string UserType { get; set; }*/
        //[Required(ErrorMessage = "Name is required")]
        [StringLength(20)]
        public string Name { get; set; }
        //[Required(ErrorMessage = "Phone Number is required")]
        [StringLength(8)]
        public string PhoneNumber { get; set; }
        //[Required(ErrorMessage = "Email is required")]
        [StringLength(30)]
        public string Email { get; set; }
        public List<Account> Accounts { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        //[Required]
        [StringLength(10)]
        public string socialNumber { get; set; }

    }
}
