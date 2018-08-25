/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public UserController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<User> getAll()
        {

            var allUsers = context.Users.ToList();

            var usList = new List<User>();

            foreach(var us in allUsers)
            {
                if(us.Accounts == null)
                {
                    usList.Add(us);
                } else
                {
                    var usAcco = context.Accounts.Where(x => x.UserId == us.Id).ToList();
                    us.Accounts = usAcco;
                    usList.Add(us);
                }
            }

            return usList;
        }

        [HttpGet("{id}", Name = ("userCreado"))]
        public IActionResult GetById(int id)
        {
            var user = context.Users.Include(x => x.Accounts).FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            bool userExist = context.Users.Any(x => x.socialNumber == user.socialNumber);
            if(userExist)
            {
                ModelState.AddModelError("socialNumber", "Social Number already exist");
            }
            if (ModelState.IsValid)
            {
                context.Users.Add(user);
                context.SaveChanges();
                return new CreatedAtRouteResult("userCreado", new { id = user.Id }, user);
            }

            return BadRequest(ModelState);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch([FromBody] User user, int id)
        {
            var us = context.Users.Find(id);

            if (us == null)
            {
                return BadRequest();
            }

            us.Name = user.Name;
            us.UserType = user.UserType;
            us.PhoneNumber = user.PhoneNumber;
            us.Email = user.Email;


            context.Users.Update(us);
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            context.Users.Remove(user);
            context.SaveChanges();
            return Ok(user);
        }
    }
}*/