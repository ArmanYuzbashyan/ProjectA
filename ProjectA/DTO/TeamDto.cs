using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectA.Models;

namespace ProjectA.DTO
{
    public class TeamDto
    {
        public int TeamId { get; set; }
        public int CountryId { get; set; }        
        public string TeamName { get; set; }
        public string ManagerName { get; set; }

        public List<string> Competitions { get; set; } = new List<string> { };
        
    }
}
