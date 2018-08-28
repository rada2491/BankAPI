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
            FavAccounts = new List<UserFavoriteAccount>();
            Payments = new List<Payments>();
        }
        
        [UserType]
        public string UserType { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(10)]
        public string socialNumber { get; set; }
        public List<Account> Accounts { get; set; }
        public List<UserFavoriteAccount> FavAccounts { get; set; }
        public List<Payments> Payments { get; set; }
    }
}
