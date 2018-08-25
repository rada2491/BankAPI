using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Models
{
    public class Currency : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value.ToString() == "Dollars" || value.ToString() == "Colons")
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Please select between: Dollars or Colons");
        }
    }
}
