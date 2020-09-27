using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectA.Models;
using ProjectA.Actions;
using ProjectA.DTO;
using ProjectA.Actions.Abstraction;

namespace ProjectA.Controllers
{
    [Route("api/teams")]
    [ApiController]

    public class TeamsController : ControllerBase
    {
        private readonly ITeamLogic _teamLogic;

        public TeamsController(ITeamLogic teamLogic)
        {
            _teamLogic = teamLogic;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetAllTeams()
        {
            return await _teamLogic.GetAll();
        }

        [HttpPost]
        public async Task<ActionResult> AddTeam(TeamDto teamDto)
        {
            var check = await _teamLogic.Add(teamDto);
            if (!check)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> EditTeams(int id, TeamDto teamDto)
        {
            var check = await _teamLogic.Edit(id, teamDto);
            if (!check)
            {
                return BadRequest();
            }
            return Ok();
        }
    

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeam(int id)
        {
            var check = await _teamLogic.Delete(id);
            if (!check)
            {
                return NotFound();
            }
            return Ok();
        }

        [Route("[action]/{id}")]
        [HttpPut] /// transfer player from team to team
        public async Task<ActionResult<IEnumerable<Player>>> Transfer(int id, Player player)
        {
            var check = await _teamLogic.Transfer(id, player);
            if (!check)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
