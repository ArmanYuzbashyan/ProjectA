using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectA.DTO;
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
        public async Task<ActionResult<IEnumerable<Competition/*GetCompDto*/>>> Get()
        {
            return await _context.Competitions
                .Include(c => c.Countries)
                .Include(t => t.TeamsLink)
                .ThenInclude(t => t.Team)
                .ToListAsync();
            //var comps = await _context.Competitions
            //    .Include(c => c.Countries)
            //    .Include(t => t.TeamsLink)
            //    .ThenInclude(tl => tl.TeamId)
            //    .ToListAsync();
            //var compList = new List<GetCompDto> { };
            //foreach(var c in comps)
            //{
            //    var countries = new List<string> { };
            //    var teams = new List<Team> { };
            //    foreach (var t in c.TeamsLink)
            //    {
            //        teams.Add(t.Team);
            //    }
            //    foreach( var n in c.Countries)
            //    {
            //        countries.Add(n.CountryName);
            //    }
            //    compList.Add
            //        (new GetCompDto {                         
            //            CompetitionId = c.CompetitionId,
            //            CompetitionName = c.CompetitionName,
            //            Countries = countries,
            //            TeamsLink = teams,
            //            Global = c.Global,
            //            Regional = c.Regional }
            //        );
            //}
            //return compList;
        }
        public async Task<bool> Post(PostCompetitionDto competitionDto)
        {
            if (string.IsNullOrWhiteSpace(competitionDto.CompetitionName)
                || competitionDto.Countries == null
                || competitionDto.Countries.Count == 0)
                
            {
                return false;
            }
            if (competitionDto.Global == true && competitionDto.Regional == true)
            {
                return false;
            }
            var checker = competitionDto.Countries.First().Region;
            if (competitionDto.Regional == true && competitionDto.Global == false)
            {                
                foreach (var c in competitionDto.Countries)
                {
                    if (checker != c.Region)
                    {
                        return false;
                    }
                }
            }
            if (competitionDto.Regional == false 
                && competitionDto.Global == false
                && competitionDto.Countries.Count > 1)
            {
                return false;
            }
            var compCountries = new List<Country> { };
            foreach (var dtoCountries in competitionDto.Countries)
            {
                var countryToAdd = await _context.Countries.FindAsync(dtoCountries.CountryId);
                compCountries.Add(countryToAdd);
            }
            if (!compCountries.Any())
            {
                return false;
            }
            var competition = new Competition
            {
                CompetitionName = competitionDto.CompetitionName,
                Countries = compCountries,
                Global = competitionDto.Global,
                Regional = competitionDto.Regional,
            };
            _context.Competitions.Add(competition);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Put (int id , PostCompetitionDto competitionDto)
        {
            if (string.IsNullOrWhiteSpace(competitionDto.CompetitionName))
            {
                return false;
            }
            var competition = await _context.Competitions.FindAsync(id);
            if (competition == null)
            {
                return false;
            }
                  
            competition.CompetitionName = competitionDto.CompetitionName;            
            await _context.SaveChangesAsync();
            return true;
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

        public async Task<bool> AddTeamToCompetition(int compId, TeamDto teamDto)
        {
            var team = await _context.Teams.FindAsync(teamDto.TeamId);
            if (team == null)
            {
                return false;
            }
            var competition = await _context.Competitions.FindAsync(compId);
            if (competition == null)
            {
                return false;
            }
            if (competition.Global == false
                && competition.Regional == false
                && team.TeamCountry != competition.Countries.First())
            {
                return false;
            }

            var relationObj = new TeamCompetition();
            relationObj.Team = team;
            relationObj.Competition = competition;

            team.CompetitionsLink.Add(relationObj);
            competition.TeamsLink.Add(relationObj);

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveTeamFromCompetition(int compId, TeamDto teamDto)
        {
            var team = await _context.Teams.FindAsync(teamDto.TeamId);
            if (team == null)
            {
                return false;
            }
            var competition = await _context.Competitions.FindAsync(compId);
            if (competition == null)
            {
                return false;
            }
            team.CompetitionsLink.Remove(team.CompetitionsLink.FirstOrDefault(c => c.CompetitionId == compId));
            competition.TeamsLink.Remove(competition.TeamsLink.FirstOrDefault(t => t.TeamId == teamDto.TeamId));
            return true;
        }

        public async Task<IEnumerable<Player>> GetCompetitionPlayers(int id)
        {
            return await _context.Competitions
                .Where(i => i.CompetitionId == id)
                .SelectMany(tl => tl.TeamsLink)
                .Select(t => t.Team)
                .SelectMany(p => p.Players)
                .ToListAsync();
        }


    }
}
