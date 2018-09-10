using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BankAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [HttpGet]
        public IActionResult userPayments()
        {
            var payList = new List<Payments>();
            var dict = new Dictionary<string, string>();

            HttpContext.User.Claims.ToList().ForEach(item => dict.Add(item.Type, item.Value));

            var idUser = dict["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];

            var user = _userManager.Users.FirstOrDefault(x => x.socialNumber == idUser);

            var payments = context.Payments.Where(x => (x.state == true) && (x.ApplicationUserId == user.Id)).ToList();

            if(payments != null)
            {
                foreach (var pay in payments)
                {
                    try
                    {
                        var service = context.Services.Find(pay.ServiceId);
                        var payUser = new Payments()
                        {
                            outBalance = pay.outBalance,
                            servicesName = service.Name,
                            ServiceId = pay.ServiceId
                        };
                        payList.Add(payUser);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                return Ok(payList);
            }

            return NotFound();
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
                    ServiceId = service.Id,
                    Currency = paym.currency
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

        [HttpPost]
        [Route("payService")]
        public IActionResult payService([FromBody] InfoPayment pay)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.socialNumber == pay.userId);
            var account = context.Accounts.FirstOrDefault(x => x.AccountNumber == pay.origin);
            var paym = context.Payments.FirstOrDefault(x => (x.ServiceId == Int32.Parse(pay.serviceId)) && (x.ApplicationUserId == user.Id));

            if(paym.Currency != account.Currency)
            {
                string url = "http://free.currencyconverterapi.com/api/v5/convert?q=USD_CRC&compact=y";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = WebRequestMethods.Http.Get;
                request.Accept = "application/json";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                string myResponse = "";
                using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    myResponse = sr.ReadToEnd();
                }

                dynamic stuff = JsonConvert.DeserializeObject(myResponse);

                float tipoCambio = stuff.USD_CRC.val;
                float colDol = 0;
                float dolCol = 0;
                
                if (paym.Currency == "Dollars")
                {
                    float accBalancConver = account.Balance / tipoCambio;
                    if(paym.outBalance < accBalancConver)
                    {
                        float colonsAmount = paym.outBalance * tipoCambio;
                        account.Balance = account.Balance - colonsAmount;
                        paym.state = false;
                        context.SaveChanges();
                        return Ok();
                    }

                    ModelState.AddModelError("noMoney", "Not enough money");
                    return BadRequest(ModelState);
                }
                else
                {
                    float accBalancConver = account.Balance * tipoCambio;
                    if (paym.outBalance < accBalancConver)
                    {
                        float dolarsAmount = paym.outBalance / tipoCambio;
                        account.Balance = account.Balance - dolarsAmount;
                        paym.state = false;
                        context.SaveChanges();
                        return Ok();
                    }

                    ModelState.AddModelError("noMoney", "Not enough money");
                    return BadRequest(ModelState);
                }
            }
            if(paym.outBalance < account.Balance)
            {
                account.Balance = account.Balance - paym.outBalance;
                paym.state = false;
                context.SaveChanges();
                return Ok();
            }
            ModelState.AddModelError("noMoney", "Not enough money");
            return BadRequest(ModelState);
        }
        
    }
}