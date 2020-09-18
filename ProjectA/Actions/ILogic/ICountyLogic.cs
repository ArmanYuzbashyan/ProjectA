using Microsoft.AspNetCore.Mvc;
using ProjectA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectA.Actions
{
    public interface ICountyLogic
    {
        Task<ActionResult<IEnumerable<Country>>> GetAll();
        Task<bool> Add(Country country);
        Task<bool> Edit(int id, Country country);
        Task<bool> Delete(int id);
    }
}
