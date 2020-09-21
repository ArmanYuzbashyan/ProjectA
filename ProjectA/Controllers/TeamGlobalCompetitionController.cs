using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectA.Actions.Abstraction;
using ProjectA.DTO;

namespace ProjectA.Controllers
{
    [Route("api/teamtoglobal")]
    [ApiController]
    public class TeamGlobalCompetitionController : ControllerBase
    {
        private readonly IGLobalCompetitionLogic _competitionLogic;

        public TeamGlobalCompetitionController(IGLobalCompetitionLogic competitionLogic)
        {
            _competitionLogic = competitionLogic;
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<bool>> AddTeamToCompetition(int id, TeamDto teamDto)
        {
            var check = await _competitionLogic.AddTeamToCompetition(id, teamDto);
            if (!check)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> RemoveTeamFromCompetition(int id, TeamDto teamDto)
        {
            var check = await _competitionLogic.RemoveTeamFromCompetition(id, teamDto);
            if (!check)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
