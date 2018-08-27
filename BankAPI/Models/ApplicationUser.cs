using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Models
{
    public class ApplicationUser : IdentityUser
    {

        public ApplicationUser()
        {
            Accounts = new List<Account>();
        }

        //[Required]
        [UserType]
        public string UserType { get; set; }
        //[Required(ErrorMessage = "Name is required")]
        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(10)]
        public string socialNumber { get; set; }
        //[Required(ErrorMessage = "Phone Number is required")]
        public List<Account> Accounts { get; set; }
        /*[Required, DataType(DataType.Password)]
        public string Password { get; set; }*/
        //[Required]
    }
}
