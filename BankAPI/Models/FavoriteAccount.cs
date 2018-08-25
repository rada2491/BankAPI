using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Models
{
    public class FavoriteAccount
    {
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string AccountNumber { get; set; }
        [Required]
        [StringLength(30)]
        public string AccountOwner { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string OwnerPhone { get; set; }
    }
}
