using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectA.Actions.Abstraction;
using ProjectA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectA.Actions
{
    public class RegionLogic : IRegionLogic
    {
        private readonly EfCoreContext _context;

        public RegionLogic(EfCoreContext context)
        {
            _context = context;
        }
        public async Task<ActionResult<IEnumerable<Region>>> GetAll()
        {
            return await _context.Regions.ToListAsync();
        }
        public async Task<bool> Add(Region region)
        {
            if (string.IsNullOrWhiteSpace(region.Name))
            {
                return false;
            }
            _context.Regions.Add(region);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Edit(int id, Region region)
        {
            if (string.IsNullOrWhiteSpace(region.Name))
            {
                return false;
            }
            var dbRegion = await _context.Regions.FindAsync(id);
            if (dbRegion == null)
            {
                return false;
            }
            dbRegion.Name = region.Name;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id)
        {
            var region = await _context.Regions.FindAsync(id);
            if (region == null)
            {
                return false;
            }
            _context.Regions.Remove(region);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
