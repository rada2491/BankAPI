using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Models
{
    public class PaymentCatcher
    {
        public string ServiceId { get; set; }
        public bool state { get; set; }
        public string ApplicationUserId { get; set; }
        public float outBalance { get; set; }
    }
}
