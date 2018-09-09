using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/payment")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaymentController : ControllerBase
    {

        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PaymentController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("userPayments")]
        public IActionResult userPayments([FromBody] string id)
        {
            return BadRequest();
        }

        [HttpPost]
        public IActionResult Post([FromBody] PaymentCatcher paym)
        {
            try
            {
                var service = context.Services.FirstOrDefault(x => x.Name == paym.ServiceId);
                var user = _userManager.Users.FirstOrDefault(x => x.socialNumber == paym.ApplicationUserId);

                var payment = new Payments
                {
                    outBalance = paym.outBalance,
                    state = paym.state,
                    ApplicationUserId = user.Id,
                    ServiceId = service.Id
                };

                context.Payments.Add(payment);
                context.SaveChanges();
                return Ok(paym);
            }
            catch (Exception)
            {
                return BadRequest(ModelState);
            }
        }
    }
}