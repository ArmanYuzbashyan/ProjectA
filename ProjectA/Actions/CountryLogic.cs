using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectA.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjectA.Actions
{
    public  class CountryLogic
    {
        private  readonly EfCoreContext _context;

        public  CountryLogic(EfCoreContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Country>>> Get()
        {
           return await _context.Countries.ToListAsync();
        }

        public async Task<bool> Post(Country country)
        {
            if (country.CountryName == null || country.Region == null)
            {
                return false;
            }
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Put (int id, Country country)
        {

            if (country.CountryName == null && country.Region == null)
            {
                return false;
            }

            var dbCountry = await _context.Countries.FindAsync(id);

            if (country.Region == null)
            {
                dbCountry.CountryName = country.CountryName;
                await _context.SaveChangesAsync();
                return true;
            }
            if (country.CountryName == null)
            {
                dbCountry.Region = country.Region;
                await _context.SaveChangesAsync();
                return true;
            }
            dbCountry.CountryName = country.CountryName;
            dbCountry.CountryName = country.CountryName;
            //_context.Entry(country).State = EntityState.Modified;
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
