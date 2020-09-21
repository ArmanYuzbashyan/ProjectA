using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectA.Actions;
using ProjectA.Actions.Abstraction;
using ProjectA.DTO;
using ProjectA.Models;

namespace ProjectA.Controllers
{
    [Route("api/global")]
    [ApiController]
    public class GlobalCompetitionsController : ControllerBase
    {
        private readonly IGLobalCompetitionLogic _competitionLogic;

        public GlobalCompetitionsController(IGLobalCompetitionLogic competitions)
        {
            _competitionLogic = competitions;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GlobalCompetition>>> GetGlobalCompetitions()
        {
            return await _competitionLogic.GetAll();
        }

        [HttpGet("{id}")] // get competition players via competition id
        public async Task<IEnumerable<Player>> GetCompetitionPlayers(int id)
        {
            return await _competitionLogic.GetCompetitionPlayers(id);
        }

        [HttpPost]
        public async Task<ActionResult> AddGlobalCompetitions(GlobalCompetitionDto competitionDto)
        {
            var check = await _competitionLogic.Add(competitionDto);
            if (!check)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditGlobalCompetitions(int id, GlobalCompetitionDto competitionDto)
        {
            var check = await _competitionLogic.Edit(id, competitionDto);
            if (!check)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGlobalCompetition(int id)
        {
            var check = await _competitionLogic.Delete(id);
            if (!check)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
