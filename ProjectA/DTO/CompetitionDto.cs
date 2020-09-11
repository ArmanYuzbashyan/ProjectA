using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectA.Models;

namespace ProjectA.DTO
{
    public class CompetitionDto
    {
        public int CompetitionId { get; set; }
        public string CompetitionName { get; set; }
        public List<Country> Countries { get; set; } = new List<Country> { };
        public bool Global { get; set; }
        public bool Regional { get; set; }
        public List<CompetitionTeamDto> Competitions { get; set; } = new List<CompetitionTeamDto> { };
    }
}
