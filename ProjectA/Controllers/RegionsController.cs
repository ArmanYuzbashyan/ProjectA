using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectA.Actions;
using ProjectA.Actions.Abstraction;
using ProjectA.Models;

namespace ProjectA.Controllers
{
    [Route("api/regions")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionLogic _regions;

        public RegionsController(IRegionLogic regions)
        {
            _regions = regions;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Region>>> GetAll()
        {
            return await _regions.GetAll();
        }
        
        [HttpPost]
        public async Task<ActionResult<bool>> Add(Region region)
        {
            var check = await _regions.Add(region);
            if (!check)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, Region region)
        {
            var check = await _regions.Edit(id, region);
            if (!check)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCountry(int id)
        {
            var check = await _regions.Delete(id);
            if (!check)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
