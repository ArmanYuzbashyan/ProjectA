using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectA.Models;
using ProjectA.Actions;
using ProjectA.DTO;

namespace ProjectA.Controllers
{
    [Route("api/tc")]
    [ApiController]
    public class TCController : ControllerBase
    {
        private readonly EfCoreContext _context;

        public TCController(EfCoreContext context)
        {
            _context = context;
        }
        [HttpPost("{id}")]
        public async Task<ActionResult<bool>> PostTC(int id, TeamDto teamDto)
        {
            var actionObject = new Logic(_context);
            var check = await actionObject.AddTeamToCompetition(id, teamDto);
            if (!check)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutTC(int id, TeamDto teamDto)
        {
            var actionObject = new Logic(_context);
            var check = await actionObject.RemoveTeamToCompetition(id, teamDto);
            if (!check)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
