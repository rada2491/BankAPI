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
    [Route("api/FavAccount")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FavoriteAccountController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FavoriteAccountController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetByUserId()
        {
            var dict = new Dictionary<string, string>();
            var favList = new List<FavOwner>();

            HttpContext.User.Claims.ToList().ForEach(item => dict.Add(item.Type, item.Value));

            var idUser = dict["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];

            var usuario = _userManager.Users.SingleOrDefault(x => x.socialNumber == idUser);

            var favAco = context.UserFavoriteAccounts.Where(x => x.ApplicationUserId == usuario.Id
            ).ToList();

            if (favAco == null)
            {
                return NotFound();
            }
            else
            {
                foreach (var user in favAco)
                {
                    try
                    {
                        var account = context.Accounts.SingleOrDefault(x => x.AccountNumber == user.FavoriteAccountId);
                        var uss = _userManager.Users.SingleOrDefault(x => x.socialNumber == account.accountOwner);

                        var favAcc = new FavOwner()
                        {
                            socialNumber = uss.socialNumber,
                            accountOwner = uss.Name,
                            accountNumber = account.AccountNumber,
                            currency = account.Currency
                        };

                        favList.Add(favAcc);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                return Ok(favList);
            }
        }

        [HttpPost]
        public IActionResult createFavorite([FromBody] UserFavoriteAccount usFav)
        {
            var accExist = context.Accounts.Any(x => x.AccountNumber == usFav.FavoriteAccountId);
            if (!accExist)
            {
                ModelState.AddModelError("accountExist", "Account not found");
                return BadRequest(ModelState);
            }
            var usFa = _userManager.Users.SingleOrDefault(x => x.socialNumber == usFav.ApplicationUserId);
            var accFav = context.UserFavoriteAccounts.Any(x => (x.FavoriteAccountId == usFav.FavoriteAccountId) && (x.ApplicationUserId == usFav.ApplicationUserId));
            FavoriteAccount fa = new FavoriteAccount();
            if (!accFav)
            {
                var exiFav = context.FavoriteAccount.Any(x => x.accountNumber == usFav.FavoriteAccountId);
                if (!exiFav)
                {
                    fa.accountNumber = usFav.FavoriteAccountId;
                    context.FavoriteAccount.Add(fa);
                    context.SaveChanges();
                }
                usFav.ApplicationUserId = usFa.Id;
                context.UserFavoriteAccounts.Add(usFav);
                context.SaveChanges();
                return Ok(usFav);
            }

            return BadRequest(ModelState);
        }
    }
}