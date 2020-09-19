using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectA.Models;
using ProjectA.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectA.Actions.Abstraction;

namespace ProjectA.Actions
{
    public class TeamLogic : ITeamLogic
    {
        private readonly EfCoreContext _context;

        public TeamLogic(EfCoreContext context)
        {
            _context = context;
        }
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetAll()
        {
            var teams = await _context.Teams
                .Include(c => c.CompetitionsLink)
                .ThenInclude(c => c.Competition)
                .Include(c =>c.TeamCountry)
                .ToListAsync();
            var teamDtos = new List<TeamDto> { };
            foreach (var t in teams)
            {
                var comptetitions = new List<string> { };
                foreach (var c in t.CompetitionsLink)
                {
                    comptetitions.Add(c.Competition.CompetitionName);
                }
                teamDtos.Add(new TeamDto
                {
                    TeamId = t.TeamId,
                    TeamName = t.TeamName,
                    ManagerName = t.ManagerName,
                    CountryId = t.TeamCountry.CountryId,
                    Competitions = comptetitions,
                });
            }
            return  teamDtos;
        }
        public async Task<bool> Add(TeamDto teamDto)
        {
            if (string.IsNullOrWhiteSpace(teamDto.TeamName)
                || string.IsNullOrWhiteSpace(teamDto.ManagerName))
            {
                return false;
            }
            var teamCountry = await _context.Countries.FindAsync(teamDto.CountryId);
            if (teamCountry == null)
            {
                return false;
            }
            var team = new Team
            {
                TeamName = teamDto.TeamName,
                ManagerName = teamDto.ManagerName,
                TeamCountry = teamCountry,
            };
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Edit(int id, TeamDto teamDto)
        {
            if (string.IsNullOrWhiteSpace(teamDto.TeamName)
                || string.IsNullOrWhiteSpace(teamDto.ManagerName))
            {
                return false;
            }            
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return false;
            }
            team.TeamName = teamDto.TeamName;
            team.ManagerName = teamDto.ManagerName;            

            await _context.SaveChangesAsync();
            return true;

            //_context.Entry(team).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
            //return true;
        }
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
        public async Task<bool> Transfer(int teamId, Player playerDto)
        {
            var team = await _context.Teams.FindAsync(teamId);
            var player = await _context.Players.FindAsync(playerDto.PlayerId);
            if (team == null || player == null)
            {
                return false;
            }
            player.Team = team;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
