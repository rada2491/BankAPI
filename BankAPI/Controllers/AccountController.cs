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
    [Route("api/Account")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountController : ControllerBase
    {

        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IEnumerable<Account> getAll()
        {
            return context.Accounts.ToList();
        }

        [HttpGet]
        [Route("useAccount")]
        public IActionResult GetByUserId()
        {

            var dict = new Dictionary<string, string>();

            HttpContext.User.Claims.ToList().ForEach(item => dict.Add(item.Type, item.Value));

            var idUser = dict["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
           
            var acco = context.Accounts.Where(x => x.accountOwner == idUser).ToList();

            if (acco == null)
            {
                return NotFound();
            }

            return Ok(acco);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Account acco)
        {
            bool accoEx = context.Accounts.Any(x => x.AccountNumber == acco.AccountNumber);
            bool userExi = _userManager.Users.Any(x => x.socialNumber == acco.accountOwner);
            if(accoEx)
            {
                ModelState.AddModelError("accountNumber", "The account number already exist.");
            }
            if (!userExi)
            {
                ModelState.AddModelError("socialNumber", "The user don't exist.");
            }
            if (ModelState.IsValid)
            {
                context.Accounts.Add(acco);
                context.SaveChanges();
                return new CreatedAtRouteResult("accountCreated", new { id = acco.Id }, acco);
            }

            return BadRequest(ModelState);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch([FromBody] Account acco, int id)
        {
            var ac = context.Accounts.Find(id);

            if (ac == null)
            {
                return BadRequest();
            }

            ac.Balance = acco.Balance;

            context.Accounts.Update(ac);
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var acc = context.Accounts.FirstOrDefault(x => x.Id == id);
            if (acc == null)
            {
                return NotFound();
            }

            context.Accounts.Remove(acc);
            context.SaveChanges();
            return Ok(acc);
        }

        [HttpPost]
        [Route("transaction")]
        public IActionResult transaction([FromBody] Transaction tran)
        {
            var ori = context.Accounts.FirstOrDefault(x => x.AccountNumber == tran.origin);
            var dest = context.Accounts.FirstOrDefault(x => x.AccountNumber == tran.destiny);

            if(ori.Balance < tran.mount)
            {
                ModelState.AddModelError("mount", "Not enough money in the origin account.");
                return BadRequest(ModelState);
            }

            ori.Balance = ori.Balance - tran.mount;
            dest.Balance = dest.Balance + tran.mount;

            context.SaveChanges();

            return Ok();
        }

    }
}