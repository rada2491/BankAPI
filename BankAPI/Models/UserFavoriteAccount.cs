using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Models
{
    public class UserFavoriteAccount
    {
        
        public string FavoriteAccountId { get; set; }
        public FavoriteAccount FavoriteAccount { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
