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
    [Route("api/Client")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserClientController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UserClientController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult getInfoUser()
        {

            var dict = new Dictionary<string, string>();

            HttpContext.User.Claims.ToList().ForEach(item => dict.Add(item.Type, item.Value));

            var idUser = dict["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];

            var user = _userManager.Users.FirstOrDefault(x => x.socialNumber == idUser);

            if(user == null)
            {
                return NotFound();
            }

            var usList = new List<User>();

            try
            {

                var usAcco = _context.Accounts.Where(x => x.accountOwner == user.socialNumber).ToList();
                user.Accounts = usAcco;

                var usSend = new User()
                {
                    Name = user.Name,
                    socialNumber = user.socialNumber,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Accounts = usAcco
                };

                usList.Add(usSend);
            }
            catch (Exception)
            {

                var usSend = new User()
                {
                    Name = user.Name,
                    socialNumber = user.socialNumber,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Accounts = user.Accounts
                };
                usList.Add(usSend);
            }

            return Ok(usList);

        }
    }
}