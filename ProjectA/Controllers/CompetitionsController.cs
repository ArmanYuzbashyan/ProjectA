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
    [Route("api/competitions")]
    [ApiController]

    public class CompetitionsController : ControllerBase
    {
        private readonly ICompetitionLogic _competitionLogic;

        public CompetitionsController(ICompetitionLogic competitionLogic)
        {
            _competitionLogic = competitionLogic;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable</*GetCompDto*/Competition>>> GetCompetitions()
        {
            return await _competitionLogic.GetAll();
        }

        [HttpPost]
        public async Task<ActionResult> AddCompetitions(PostCompetitionDto competitionDto)
        {
            var check = await _competitionLogic.Add(competitionDto);
            if (!check)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditCompetitions(int id, PostCompetitionDto competitionDto)
        {
            var check = await _competitionLogic.Edit(id,competitionDto);
            if (!check)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCompetition(int id)
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
