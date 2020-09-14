using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectA.Models;
using ProjectA.DTO;

namespace ProjectA.Actions
{
    public class Logic
    {
        private readonly EfCoreContext _context;

        public Logic(EfCoreContext context)
        {
            _context = context;
        }

        //public async Task<IEnumerable<PlayerDto>> GetCompetitionPlayers (int id)
        //{
        //    return await ;
        //}
        //public async Task<bool> Transfer (int teamId, Player player)
        //{
        //    return await;
        //}
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
        public async Task<bool> RemoveTeamToCompetition(int compId, TeamDto teamDto)
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
