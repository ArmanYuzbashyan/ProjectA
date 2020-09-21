using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectA.Actions.Abstraction;
using ProjectA.DTO;
using ProjectA.Models;

namespace ProjectA.Actions
{
    public class GlobalCompetitionLogic : IGLobalCompetitionLogic
    {
        private readonly EfCoreContext _context;

        public GlobalCompetitionLogic(EfCoreContext context)
        {
            _context = context;
        }
        public async Task<ActionResult<IEnumerable<GlobalCompetition>>> GetAll()
        {
            return await _context.GlobalCompetitions
                .Include(c => c.Region)
                .Include(t => t.TeamsLink)
                .ThenInclude(t => t.Team)
                .ToListAsync();            
        }
        public async Task<bool> Add(GlobalCompetitionDto competitionDto)
        {
            if (string.IsNullOrWhiteSpace(competitionDto.Name))                
            {
                return false;
            }
            var region = await _context.Regions.FindAsync(competitionDto.RegionId);
            if (region == null)
            {
                return false;
            }
            
            var competition = new GlobalCompetition
            {
                Name = competitionDto.Name,
                Region = region,               
            };
            _context.GlobalCompetitions.Add(competition);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Edit (int id , GlobalCompetitionDto competitionDto)
        {
            if (string.IsNullOrWhiteSpace(competitionDto.Name))
            {
                return false;
            }
            var competition = await _context.GlobalCompetitions.FindAsync(id);
            if (competition == null)
            {
                return false;
            }
                  
            competition.Name = competitionDto.Name;            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var competition = await _context.GlobalCompetitions.FindAsync(id);
            if (competition == null)
            {
                return false;
            }
            _context.GlobalCompetitions.Remove(competition);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddTeamToCompetition(int compId, TeamDto teamDto)
        {
            var team = await _context.Teams.FindAsync(teamDto.Id);
            if (team == null)
            {
                return false;
            }
            var competition = await _context.GlobalCompetitions.FindAsync(compId);
            if (competition == null)
            {
                return false;
            }
            if (team.Country.Region != competition.Region
                && competition.Region.Name != "World")
            {
                return false;
            }

            var relationObj = new TeamGlobalCompetition()
            {
                Team = team,
                Competition = competition
            };

            team.GlobalCompetitionsLink.Add(relationObj);
            competition.TeamsLink.Add(relationObj);

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveTeamFromCompetition(int compId, TeamDto teamDto)
        {
            var team = await _context.Teams.FindAsync(teamDto.Id);
            if (team == null)
            {
                return false;
            }
            var competition = await _context.GlobalCompetitions.FindAsync(compId);
            if (competition == null)
            {
                return false;
            }
            team.GlobalCompetitionsLink.Remove(team.GlobalCompetitionsLink.SingleOrDefault(c => c.CompetitionId == compId));
            competition.TeamsLink.Remove(competition.TeamsLink.SingleOrDefault(t => t.TeamId == teamDto.Id));
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Player>> GetCompetitionPlayers(int id)
        {
            return await _context.GlobalCompetitions
                .Where(i => i.Id == id)
                .SelectMany(tl => tl.TeamsLink)
                .Select(t => t.Team)
                .SelectMany(p => p.Players)
                .ToListAsync();
        }
    }
}
