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
    [Route("api/players")]
    [ApiController]

    public class PlayersController : ControllerBase
    {
        private readonly EfCoreContext _context;

        public PlayersController(EfCoreContext context)
        {
            _context = context;
        }

    }
}
