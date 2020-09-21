using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectA.Models;

namespace ProjectA.DTO
{
    public class TeamDto
    {
        public int Id { get; set; }
        public int CountryId { get; set; }        
        public string Name { get; set; }
        public string Manager { get; set; }

        public List<string> NationalCompetitions { get; set; } = new List<string> { };
        public List<string> GlobalCompetitions { get; set; } = new List<string> { };

    }
}
