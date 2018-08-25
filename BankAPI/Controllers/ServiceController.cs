using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Services")]
    [ApiController]
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

    }
}