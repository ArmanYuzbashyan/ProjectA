using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectA.Models
{
    public class Competition
    {
        public Guid CompertitionId { get; set; }
        public string CompetitionName { get; set; }
        public ICollection<Country> Countries { get; set; }
        public bool Global { get; set; }
        public bool Regional { get; set; }


        public ICollection<TeamCompetition> MemberTeamsLink { get; set; } 
    }
}
