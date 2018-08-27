using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Models
{
    public class Transaction
    {
        public string origin { get; set; }
        public string destiny { get; set; }
        public float mount { get; set; }
    }
}
