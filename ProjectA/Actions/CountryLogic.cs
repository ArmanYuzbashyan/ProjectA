using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectA.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjectA.Actions
{
    public  class CountryLogic : ICountyLogic
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

        public async Task<bool> Add(Country country)
        {
            if (country.CountryName == null || country.Region == null)
            {
                return false;
            }
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Edit (int id, Country country) ///better put function
        {

            if (string.IsNullOrWhiteSpace(country.CountryName) || string.IsNullOrWhiteSpace(country.Region))
            {
                return false;
            }

            var dbCountry = await _context.Countries.FindAsync(id);

            if (dbCountry == null)
            {
                return false;
            }
            
            dbCountry.CountryName = country.CountryName;
            dbCountry.Region = country.Region;            
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
