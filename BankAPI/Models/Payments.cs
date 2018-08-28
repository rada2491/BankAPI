using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Models
{
    public class Payments
    {

        [Required]
        public float outBalance { get; set; }
        [Required]
        public bool state { get; set; }

        
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
