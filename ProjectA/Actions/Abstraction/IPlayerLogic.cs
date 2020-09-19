using Microsoft.AspNetCore.Mvc;
using ProjectA.DTO;
using ProjectA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectA.Actions.Abstraction
{
    public interface IPlayerLogic
    {
        Task<ActionResult<IEnumerable<Player>>> GetAll();
        Task<bool> Add(PlayerDto playerDto);
        Task<bool> Edit(int id, PlayerDto playerDto);
        Task<bool> Delete(int id);
    }
}
