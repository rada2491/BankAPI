using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPI.Models
{
    public class UserType : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null)
            {
                value = "Client";
                return ValidationResult.Success;
            }

            if (value.ToString() == "Admin" || value.ToString() == "Client")
            {
                return ValidationResult.Success;
            }


            return new ValidationResult("The type of user must be Client or Admin");
        }
    }
}
