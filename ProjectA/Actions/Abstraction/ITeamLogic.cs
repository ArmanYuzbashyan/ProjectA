using Microsoft.AspNetCore.Mvc;
using ProjectA.DTO;
using ProjectA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectA.Actions.Abstraction
{
    public interface ITeamLogic
    {
        Task<ActionResult<IEnumerable<TeamDto>>> GetAll();
        Task<bool> Add(TeamDto teamDto);
        Task<bool> Edit(int id, TeamDto teamDto);
        Task<bool> Delete(int id);
        Task<bool> Transfer(int teamId, Player playerDto);
    }
}
