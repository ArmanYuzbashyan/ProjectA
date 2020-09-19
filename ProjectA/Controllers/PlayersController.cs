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
    [Route("api/players")]
    [ApiController]

    public class PlayersController : ControllerBase
    {
        private readonly IPlayerLogic _playerLogic;

        public PlayersController(IPlayerLogic playerLogic)
        {
            _playerLogic = playerLogic;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            return await _playerLogic.GetAll();
        }

        [HttpPost]
        public async Task<ActionResult> AddPlayer(PlayerDto playerDto)
        {
            var check = await _playerLogic.Add(playerDto);
            if (!check)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(int id, PlayerDto playerDto)
        {
           var check = await _playerLogic.Edit(id, playerDto);
            if (!check)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCountry(int id)
        {
            var check = await _playerLogic.Delete(id);
            if (!check)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
