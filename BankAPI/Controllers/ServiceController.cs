using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Services")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ServiceController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ServiceController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Service> getAll()
        {
            return context.Services.ToList();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Service serv)
        {


            if (ModelState.IsValid)
            {
                context.Services.Add(serv);
                context.SaveChanges();
                return Ok(serv);
            }

            return BadRequest(ModelState);
        }



    }
}