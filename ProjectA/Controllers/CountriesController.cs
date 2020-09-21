using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectA.Models;
using ProjectA.Actions;
using ProjectA.DTO;

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
        public async Task<ActionResult<IEnumerable<Country>>> GetAllCountries()
        {
            return await _countries.GetAll();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCountry(int id, CountryDto countryDto)
        {
            var check = await _countries.Edit(id, countryDto);
            if (!check)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Country>> AddCountry(CountryDto countryDto)
        {            
            var check = await _countries.Add(countryDto);
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
