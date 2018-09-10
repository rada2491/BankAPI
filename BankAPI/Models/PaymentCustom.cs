using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Models
{
    public class PaymentCustom
    {
        public string ServiceName { get; set; }
        public string ServiceId { get; set; }
        public string outBalance { get; set; }
    }
}
