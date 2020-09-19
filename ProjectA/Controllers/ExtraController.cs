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
    [Route("api/extra")]
    [ApiController]
    public class ExtraController : ControllerBase
    {
        private readonly ICompetitionLogic _competitionLogic;
        private readonly ITeamLogic _teamLogic;

        public ExtraController(ICompetitionLogic competitionLogic, ITeamLogic teamLogic)
        {
            _competitionLogic = competitionLogic;
            _teamLogic = teamLogic;
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<Player>> GetCompetitionPlayers(int id)
        {
            return await _competitionLogic.GetCompetitionPlayers(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Player>>> TransferPlayer(int id, Player player)
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
