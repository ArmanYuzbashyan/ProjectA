using ProjectA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectA.DTO
{
    public class CompetitionTeamDto
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public Country TeamCountry { get; set; }
    }
}
