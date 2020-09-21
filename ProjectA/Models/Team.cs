using ProjectA.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectA.Models
{
    public class Team
    {
        public Team()
        {
            NationalCompetitionsLink = new HashSet<TeamNationalCompetition>();
            GlobalCompetitionsLink = new HashSet<TeamGlobalCompetition>();
            Players = new HashSet<Player>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Manager { get; set; }
        public Country Country { get; set; }

        public ICollection<Player> Players { get; set; }
        public ICollection<TeamNationalCompetition> NationalCompetitionsLink { get; set; }
        public ICollection<TeamGlobalCompetition> GlobalCompetitionsLink { get; set; }
    }
}
