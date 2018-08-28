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

        [HttpGet("{id}")]
        public IActionResult GetByUserId(int id)
        {
            //corregir
            var favAco = context.UserFavoriteAccounts.Where(x => x.ApplicationUserId == id.ToString()).ToList();

            if (favAco == null)
            {
                return NotFound();
            }

            return Ok(favAco);
        }

        [HttpPost]
        public IActionResult createFavorite([FromBody] UserFavoriteAccount usFav)
        {
            var accExist = context.Accounts.Any(x => x.AccountNumber == usFav.FavoriteAccountId);
            if(!accExist)
            {
                ModelState.AddModelError("accountExist", "Account not found");
                
            }
            var accFav = context.FavoriteAccount.Any(x => x.accountNumber == usFav.FavoriteAccountId);
            if (!accFav)
            {
                //aqui crea la favorita y la inserta
            }

        return BadRequest(ModelState);
        }
    }
}