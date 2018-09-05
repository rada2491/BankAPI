using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Models
{
    public class FavOwner
    {
        public string socialNumber { get; set; }
        public string accountNumber { get; set; }
        public string accountOwner { get; set; }
        public string currency { get; set; }
    }
}
