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
    [Route("api/teams")]
    [ApiController]

    public class TeamsController : ControllerBase
    {
        private readonly EfCoreContext _context;

        public TeamsController(EfCoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetTeams()
        {
            var actionObject = new TeamLogic(_context);
            return await actionObject.Get();
        }
        [HttpPost]
        public async Task<ActionResult> PostTeam(TeamDto teamDto)
        {
            var actionObject = new TeamLogic(_context);
            var check = await actionObject.Post(teamDto);
            if (!check)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> PutTeams(int id, TeamDto teamDto)
        {
            var actionObject = new TeamLogic(_context);
            var check = await actionObject.Put(id, teamDto);
            if (!check)
            {
                return BadRequest();
            }
            return Ok();
        }
    

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeam(int id)
        {
            var actionObject = new TeamLogic(_context);
            var check = await actionObject.Delete(id);
            if (!check)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
