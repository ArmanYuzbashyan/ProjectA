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
        private readonly ICountryLogic _countries;        

        public CountriesController( ICountryLogic countries)
        {
            _countries = countries;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            return await _countries.GetAll();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, Country country)
        {
            var check = await _countries.Edit(id, country);
            if (!check)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(Country country)
        {            
            var check = await _countries.Add(country);
            if (!check)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCountry(int id)
        {
            var check = await _countries.Delete(id);
            if (!check)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}




//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using ProjectA.Models;

//namespace ProjectA.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CountriesController : ControllerBase
//    {
//        private readonly EfCoreContext _context;

//        public CountriesController(EfCoreContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Countries
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
//        {
//            return await _context.Countries.ToListAsync();
//        }

//        // GET: api/Countries/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Country>> GetCountry(int id)
//        {
//            var country = await _context.Countries.FindAsync(id);

//            if (country == null)
//            {
//                return NotFound();
//            }

//            return country;
//        }

//        // PUT: api/Countries/5
//        
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutCountry(int id, Country country)
//        {
//            if (id != country.CountryId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(country).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!CountryExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Countries
//        
//        [HttpPost]
//        public async Task<ActionResult<Country>> PostCountry(Country country)
//        {
//            _context.Countries.Add(country);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetCountry", new { id = country.CountryId }, country);
//        }

//        // DELETE: api/Countries/5
//        [HttpDelete("{id}")]
//        public async Task<ActionResult<Country>> DeleteCountry(int id)
//        {
//            var country = await _context.Countries.FindAsync(id);
//            if (country == null)
//            {
//                return NotFound();
//            }

//            _context.Countries.Remove(country);
//            await _context.SaveChangesAsync();

//            return country;
//        }

//        private bool CountryExists(int id)
//        {
//            return _context.Countries.Any(e => e.CountryId == id);
//        }
//    }
//}
