using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectA.Models;
using ProjectA.Actions;

namespace ProjectA.Controllers
{
    [Route("api/countries")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly EfCoreContext _context;

        public CountriesController(EfCoreContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            var actionObject = new CountryLogic(_context);
            return await actionObject.Get();            
        }
        
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Country>> GetCountry(int id)
        //{
        //    var country = await _context.Countries.FindAsync(id);

        //    if (country == null)
        //    {
        //        return NotFound();
        //    }

        //    return country;
        //}


        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, Country country)
        {
            var actionObject = new CountryLogic(_context);
            var check = await actionObject.Put(id, country);
            if (!check)
                return BadRequest();
            return Ok();
        }
        
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(Country country)
        {
            var actionObject = new CountryLogic(_context);
            var check = await actionObject.Post(country);
            if (!check)
                return BadRequest();            
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCountry(int id)
        {
            var actionObject = new CountryLogic(_context);
            var check = await actionObject.Delete(id);
            if (!check)
                return NotFound();
            return Ok();
        }
    }
}
