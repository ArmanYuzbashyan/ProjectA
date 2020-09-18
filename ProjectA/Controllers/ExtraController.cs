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
    [Route("api/extra")]
    [ApiController]
    public class ExtraController : ControllerBase
    {
        private readonly EfCoreContext _context;

        public ExtraController(EfCoreContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<Player>> GetCompetitionPlayers(int id)
        {
            var actionObj = new CompetitionLogic(_context);
            return await actionObj.GetCompetitionPlayers(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Player>>> TransferPlayer(int id, Player player)
        {
            var actionObj = new TeamLogic(_context);
            var check = await actionObj.Transfer(id, player);
            if (!check)
            {
                return BadRequest();
            }
            return Ok();
        }

    }
}
