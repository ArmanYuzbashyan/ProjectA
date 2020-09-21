using Microsoft.AspNetCore.Mvc;
using ProjectA.DTO;
using ProjectA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectA.Actions.Abstraction
{
    public interface INationalCompetitionLogic
    {
        Task<ActionResult<IEnumerable<NationalCompetition>>> GetAll();
        Task<bool> Add(NationalCompetitionDto competitionDto);
        Task<bool> Edit(int id, NationalCompetitionDto competitionDto);
        Task<bool> Delete(int id);
        Task<bool> AddTeamToCompetition(int compId, TeamDto teamDto);
        Task<bool> RemoveTeamFromCompetition(int compId, TeamDto teamDto);
        Task<IEnumerable<Player>> GetCompetitionPlayers(int id);
    }
}
