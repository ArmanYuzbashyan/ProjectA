using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectA.Models;

namespace ProjectA.Actions
{
    public class CompetitionLogic
    {
        private readonly EfCoreContext _context;

        public CompetitionLogic(EfCoreContext context)
        {
            _context = context;
        }
        public async Task<ActionResult<IEnumerable<Competition>>> Get()
        {
            var competitions = _context.Competitions
                .Include(c => c.Countries)
                .Include(t => t.TeamsLink)
                .ThenInclude(t => t.Team)
                .ToListAsync();
                
            return await competitions;
        }


        public async Task<bool> Delete(int id)
        {
            var competition = await _context.Competitions.FindAsync(id);
            if (competition == null)
            {
                return false;
            }
            _context.Competitions.Remove(competition);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
