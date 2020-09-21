using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectA.Models
{
    public class NationalCompetition
    {
        public NationalCompetition()
        {
            TeamsLink = new HashSet<TeamNationalCompetition>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
        public ICollection<TeamNationalCompetition> TeamsLink { get; set; } 
    }
}
