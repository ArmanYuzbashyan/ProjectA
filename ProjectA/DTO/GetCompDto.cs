using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectA.Models;


namespace ProjectA.DTO
{
    public class GetCompDto
    {
        public int CompetitionId { get; set; }
        public string CompetitionName { get; set; }
        public List<string> Countries { get; set; } = new List<string> { };
        public bool Global { get; set; }
        public bool Regional { get; set; }
        public List<Team> TeamsLink { get; set; } = new List<Team> { };
    }
}
