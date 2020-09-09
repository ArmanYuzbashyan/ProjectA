using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectA.Models;
//using ProjectA.Actions;

namespace ProjectA.Controllers
{
    [Route("api/teams")]
    [ApiController]

    public class TeamsController : ControllerBase
    {
        private readonly EfCoreContext _context;

        public TeamsController(EfCoreContext context)
        {
            _context = context;
        }




    }
}
