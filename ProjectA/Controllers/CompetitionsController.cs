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
    [Route("api/competitions")]
    [ApiController]

    public class CompetitionsController : ControllerBase
    {
        private readonly EfCoreContext _context;

        public CompetitionsController(EfCoreContext context)
        {
            _context = context;
        }


    }
}
