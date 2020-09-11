using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectA.Models;
using ProjectA.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProjectA.Actions
{
    public class TeamLogic
    {
        private readonly EfCoreContext _context;

        public TeamLogic(EfCoreContext context)
        {
            _context = context;
        }
        public async Task<ActionResult<IEnumerable<Team>>> Get()
        {
            var teams = _context.Teams
                .Include(c => c.CompetitionsLink)
                .ThenInclude(c => c.Competition)
                .ToListAsync(); 
            return await teams;
        }
        //public async Task<bool> Post(TeamDto teamDto)
        //{
        //    if(teamDto.TeamName == null)
        //    {
        //        return false;
        //    }
        //    var teamCountry = await _context.Countries.FindAsync(teamDto.CountryId);
        //    if (teamCountry == null)
        //    {
        //        return false;
        //    }
        //    var teamCompetition =  await _context.Tea



        //    var team = new Team
        //    {
        //        TeamName = teamDto.TeamName,
        //        ManagerName = teamDto.Manager,
        //        TeamCountry = teamCountry,
        //        CompetitionsLink = 
        //    }

        //}


        public async Task<bool> Delete(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return false;
            }
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
