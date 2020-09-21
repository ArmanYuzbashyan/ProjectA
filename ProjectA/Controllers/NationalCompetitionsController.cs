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
    [Route("api/national")]
    [ApiController]

    public class NationalCompetitionsController : ControllerBase
    {
        private readonly INationalCompetitionLogic _competitionLogic;

        public NationalCompetitionsController(INationalCompetitionLogic competitionLogic)
        {
            _competitionLogic = competitionLogic;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NationalCompetition>>> GetNationalCompetitions()
        {
            return await _competitionLogic.GetAll();
        }

        [HttpGet("{id}")] // get competition players via competition id
        public async Task<IEnumerable<Player>> GetCompetitionPlayers(int id)
        {
            return await _competitionLogic.GetCompetitionPlayers(id);
        }

        [HttpPost]
        public async Task<ActionResult> AddNationalCompetitions(NationalCompetitionDto competitionDto)
        {
            var check = await _competitionLogic.Add(competitionDto);
            if (!check)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditNationalCompetitions(int id, NationalCompetitionDto competitionDto)
        {
            var check = await _competitionLogic.Edit(id,competitionDto);
            if (!check)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNationalCompetition(int id)
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
