using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BankAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace BankAPI.Controllers
{
    [Route("api/Autho")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        //private readonly ApplicationDbContext _context;
        //private readonly IdentityDbContext _role;

        //private ApplicationDbContext _app;
        //private readonly ApplicationDbContext _context;

        public AuthorizeController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration/*,
            ApplicationDbContext context*/)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            //_context = context;
        }

        /*[HttpGet]
        public IActionResult getAll()
        {
            var user = _userManager.Users.ToList();
            var usList = new List<User>();

            foreach (var us in user)
            {
                try
                {
                    var usAcco = _context.Accounts.Where(x => x.accountOwner == us.socialNumber).ToList();
                    us.Accounts = usAcco;
                    
                    var usSend = new User()
                    {
                        Name = us.Name,
                        socialNumber = us.socialNumber,
                        Email = us.Email,
                        PhoneNumber = us.PhoneNumber,
                        Accounts = usAcco
                    };

                    usList.Add(usSend);
                }
                catch (Exception)
                {
                    var usSend = new User()
                    {
                        Name = us.Name,
                        socialNumber = us.socialNumber,
                        Email = us.Email,
                        PhoneNumber = us.PhoneNumber,
                        Accounts = us.Accounts
                    };
                    usList.Add(usSend);
                }

            }
            return Ok(usList);

        }*/

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User model)
        {

            //check if social number exist
            bool usr = _userManager.Users.Any(x => x.socialNumber == model.socialNumber);
            if (usr)
            {
                ModelState.AddModelError("socialNumber", "Social Number already exist.");
            }

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                    Accounts = model.Accounts,
                    socialNumber = model.socialNumber,
                    PhoneNumber = model.PhoneNumber,
                    UserType = model.UserType
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return BuildToken(model);
                }
                else
                {
                    return BadRequest("Username or password invalid");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] User userInfo)
        {
            bool exist = _userManager.Users.Any(x => x.Email == userInfo.Email);
            if (!exist)
            {
                return NotFound();
            }
            var autho = _userManager.Users.FirstOrDefault(x => x.Email == userInfo.Email);
            userInfo.UserType = autho.UserType;
            userInfo.socialNumber = autho.socialNumber;
            userInfo.Name = autho.Name;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return BuildToken(userInfo);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        private IActionResult BuildToken(User userInfo)
        {

              var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim("miValor", "Lo que yo quiera"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Secret_Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(20);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: "yourdomain.com",
               audience: "yourdomain.com",
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            //add id_user in body
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                Authorization = userInfo.UserType,
                Id = userInfo.socialNumber,
                userEmail = userInfo.Email,
                userName = userInfo.Name
            });


        }
    }
}