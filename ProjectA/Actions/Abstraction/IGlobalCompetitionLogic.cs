using Microsoft.AspNetCore.Mvc;
using ProjectA.DTO;
using ProjectA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectA.Actions.Abstraction
{
    public interface IGLobalCompetitionLogic
    {
        Task<ActionResult<IEnumerable<GlobalCompetition>>> GetAll();
        Task<bool> Add(GlobalCompetitionDto competitionDto);
        Task<bool> Edit(int id, GlobalCompetitionDto competitionDto);
        Task<bool> Delete(int id);
        Task<bool> AddTeamToCompetition(int compId, TeamDto teamDto);
        Task<bool> RemoveTeamFromCompetition(int compId, TeamDto teamDto);
        Task<IEnumerable<Player>> GetCompetitionPlayers(int id);
    }
}
