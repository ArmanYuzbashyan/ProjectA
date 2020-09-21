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
        public async Task<ActionResult<IEnumerable<Team>>> GetAll()
        {
            var teams = await _context.Teams
                .Include(c => c.NationalCompetitionsLink)
                .ThenInclude(c => c.Competition)
                .Include(c => c.GlobalCompetitionsLink)
                .ThenInclude(c => c.Competition)
                .Include(c =>c.Country)
                .ToListAsync();
            
            return  teams;
        }
        public async Task<bool> Add(TeamDto teamDto)
        {
            if (string.IsNullOrWhiteSpace(teamDto.Name)
                || string.IsNullOrWhiteSpace(teamDto.Manager))
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
                Name = teamDto.Name,
                Manager = teamDto.Manager,
                Country = teamCountry,
            };
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Edit(int id, TeamDto teamDto)
        {
            if (string.IsNullOrWhiteSpace(teamDto.Name)
                || string.IsNullOrWhiteSpace(teamDto.Manager))
            {
                return false;
            }            
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return false;
            }
            team.Name = teamDto.Name;
            team.Manager = teamDto.Manager;            

            await _context.SaveChangesAsync();
            return true;
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
            var player = await _context.Players.FindAsync(playerDto.Id);
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
