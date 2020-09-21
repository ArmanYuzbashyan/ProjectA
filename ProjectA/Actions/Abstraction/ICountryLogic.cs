using Microsoft.AspNetCore.Mvc;
using ProjectA.DTO;
using ProjectA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectA.Actions
{
    public interface ICountryLogic
    {
        Task<ActionResult<IEnumerable<Country>>> GetAll();
        Task<bool> Add(CountryDto countryDto);
        Task<bool> Edit(int id, CountryDto countryDto);
        Task<bool> Delete(int id);
    }
}
