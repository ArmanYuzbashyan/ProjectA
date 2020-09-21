using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectA.Models;
using Microsoft.EntityFrameworkCore;
using ProjectA.DTO;

namespace ProjectA.Actions
{
    public  class CountryLogic : ICountryLogic
    {
        private  readonly EfCoreContext _context;

        public  CountryLogic(EfCoreContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Country>>> GetAll()
        {
           return await _context.Countries.ToListAsync();
        }

        public async Task<bool> Add(CountryDto countryDto)
        {
            if (string.IsNullOrWhiteSpace(countryDto.Name))
            {
                return false;
            }
            var region = await _context.Regions.FindAsync(countryDto.RegionId);
            if (region == null || region.Name == "World")
            {
                return false;
            }
            var country = new Country
            {
                Name = countryDto.Name,
                Region = region
            };
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Edit(int id, CountryDto countryDto)
        {

            if (string.IsNullOrWhiteSpace(countryDto.Name))
            {
                return false;
            }

            var dbCountry = await _context.Countries.FindAsync(id);

            if (dbCountry == null)
            {
                return false;
            }
            
            dbCountry.Name = countryDto.Name;                        
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete (int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return false;
            }
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
