using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Models
{
    public class FavoriteAccount
    {
        public FavoriteAccount()
        {
            UserFavoriteAccount = new List<UserFavoriteAccount>();
        }

        [Key]
        public string accountNumber { get; set; }

        public List<UserFavoriteAccount> UserFavoriteAccount { get; set; }
    }
}
