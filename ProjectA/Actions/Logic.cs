using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectA.Models;
using ProjectA.DTO;
using Microsoft.EntityFrameworkCore;

namespace ProjectA.Actions
{
    public class Logic
    {
        private readonly EfCoreContext _context;

        public Logic(EfCoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Player>> GetCompetitionPlayers(int id)
        {
            List<Player> players = new List<Player> { };
            var competition = await _context.Competitions.FindAsync(id);
            if(competition == null)
            {
                return players;
            }
            var allPlayers = await _context.Players.ToListAsync();
            foreach (var t in competition.TeamsLink)
            {
                foreach(var p in allPlayers )
                {
                    if (t.Team.TeamId == p.Team.TeamId)
                    {
                        players.Add(p);
                    }
                }
            }
            return players;
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
        public async Task<bool> AddTeamToCompetition (int compId, TeamDto teamDto)
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
    }
}
