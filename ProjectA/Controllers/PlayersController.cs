using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectA.Models;
using ProjectA.Actions;

namespace ProjectA.Controllers
{
    [Route("api/players")]
    [ApiController]

    public class PlayersController : ControllerBase
    {
        private readonly EfCoreContext _context;

        public PlayersController(EfCoreContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            var actionObject = new PlayerLogic(_context);
            return await actionObject.Get();
        }
        [HttpPost]
        public async Task<ActionResult> PostPlayer(Player player)
        {
            var actionObject = new PlayerLogic(_context);
            var check = await actionObject.Post(player);
            if (!check)
                return BadRequest();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCountry(int id)
        {
            var actionObject = new PlayerLogic(_context);
            var check = await actionObject.Delete(id);
            if (!check)
                return NotFound();
            return Ok();
        }
    }
}
