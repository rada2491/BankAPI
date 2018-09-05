using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Models
{
    public class Service
    {

        public Service()
        {
            Payments = new List<Payments>();
        }
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        public bool Active { get; set; }
        [Required]
        public string Description { get; set; }

        public List<Payments> Payments { get; set; }
    }
}
