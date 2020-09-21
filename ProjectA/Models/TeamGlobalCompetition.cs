using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectA.Models
{
    public class TeamGlobalCompetition
    {
        public int TeamId { get; set; }
        public int CompetitionId { get; set; }

        public Team Team { get; set; }
        public GlobalCompetition Competition { get; set; }
    }
}
