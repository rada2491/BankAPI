using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Models
{
    public class Account
    {
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string AccountNumber { get; set; }
        [Currency]
        public string Currency { get; set; }
        public float Balance { get; set; }
        [Required]
        [StringLength(10)]
        public string accountOwner { get; set; }
        
    }
}
