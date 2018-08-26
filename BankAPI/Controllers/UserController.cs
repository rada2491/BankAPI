﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UserController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
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

        }
    }
}