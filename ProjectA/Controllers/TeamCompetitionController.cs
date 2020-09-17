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
    [Route("api/teamcompetition")]
    [ApiController]
    public class TeamCompetitionController : ControllerBase
    {
        private readonly EfCoreContext _context;

        public TeamCompetitionController(EfCoreContext context)
        {
            _context = context;
        }
        [HttpPost("{id}")]
        public async Task<ActionResult<bool>> AddTeamToCompetition(int id, TeamDto teamDto)
        {
            var actionObject = new CompetitionLogic(_context);
            var check = await actionObject.AddTeamToCompetition(id, teamDto);
            if (!check)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> RemoveTeamFromCompetition(int id, TeamDto teamDto)
        {
            var actionObject = new CompetitionLogic(_context);
            var check = await actionObject.RemoveTeamFromCompetition(id, teamDto);
            if (!check)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
