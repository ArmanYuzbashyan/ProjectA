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
    [Route("api/competitions")]
    [ApiController]

    public class CompetitionsController : ControllerBase
    {
        private readonly EfCoreContext _context;

        public CompetitionsController(EfCoreContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable</*GetCompDto*/Competition>>> GetCompetitions()
        {
            var actionObject = new CompetitionLogic(_context);
            return await actionObject.Get();
        }
        [HttpPost]
        public async Task<ActionResult> PostCompetitions(PostCompetitionDto competitionDto)
        {
            var actionObject = new CompetitionLogic(_context);
            var check = await actionObject.Post(competitionDto);
            if (!check)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutCompetitions(int id, PostCompetitionDto competitionDto)
        {
            var actionObject = new CompetitionLogic(_context);
            var check = await actionObject.Put(id,competitionDto);
            if (!check)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCompetition(int id)
        {
            var actionObject = new CompetitionLogic(_context);
            var check = await actionObject.Delete(id);
            if (!check)
            {
                return NotFound();
            }
            return Ok();
        }
       
    }
}
