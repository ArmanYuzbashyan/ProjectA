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
    public class NationalCompetitionLogic : INationalCompetitionLogic
    {
        private readonly EfCoreContext _context;

        public NationalCompetitionLogic(EfCoreContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<NationalCompetition>>> GetAll()
        {
            return await _context.NationalCompetitions
                .Include(c => c.Country)
                .Include(t => t.TeamsLink)
                .ThenInclude(t => t.Team)
                .ToListAsync();           
        }

        public async Task<bool> Add(NationalCompetitionDto competitionDto)
        {
            if (string.IsNullOrWhiteSpace(competitionDto.Name))  
            {
                return false;
            }
            var country = await _context.Countries.FindAsync(competitionDto.CountryId);
            if (country == null)
            {
                return false;
            }            
            var competition = new NationalCompetition
            {
                Name = competitionDto.Name,
                Country = country,
            };
            _context.NationalCompetitions.Add(competition);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Edit (int id , NationalCompetitionDto competitionDto)
        {
            if (string.IsNullOrWhiteSpace(competitionDto.Name))
            {
                return false;
            }
            var competition = await _context.NationalCompetitions.FindAsync(id);
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
            var competition = await _context.NationalCompetitions.FindAsync(id);
            if (competition == null)
            {
                return false;
            }
            _context.NationalCompetitions.Remove(competition);
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
            var competition = await _context.NationalCompetitions.FindAsync(compId);
            if (competition == null)
            {
                return false;
            }
            if (team.Country != competition.Country)
            {
                return false;
            }

            var relationObj = new TeamNationalCompetition()
            {
                Team = team,
                Competition = competition
            };

            team.NationalCompetitionsLink.Add(relationObj);
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
            var competition = await _context.NationalCompetitions.FindAsync(compId);
            if (competition == null)
            {
                return false;
            }
            team.NationalCompetitionsLink.Remove(team.NationalCompetitionsLink.SingleOrDefault(c => c.CompetitionId == compId));
            competition.TeamsLink.Remove(competition.TeamsLink.SingleOrDefault(t => t.TeamId == teamDto.Id));

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Player>> GetCompetitionPlayers(int id)
        {
            return await _context.NationalCompetitions
                .Where(i => i.Id == id)
                .SelectMany(tl => tl.TeamsLink)
                .Select(t => t.Team)
                .SelectMany(p => p.Players)
                .ToListAsync();
        }
    }
}
