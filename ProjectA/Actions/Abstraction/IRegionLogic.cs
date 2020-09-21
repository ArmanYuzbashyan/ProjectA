using Microsoft.AspNetCore.Mvc;
using ProjectA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectA.Actions.Abstraction
{
    public interface IRegionLogic
    {
        Task<ActionResult<IEnumerable<Region>>> GetAll();
        Task<bool> Add(Region region);
        Task<bool> Edit(int id, Region region);
        Task<bool> Delete(int id);
    }
}
