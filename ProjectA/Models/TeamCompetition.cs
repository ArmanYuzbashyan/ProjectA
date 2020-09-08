using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectA.Models
{
    public class TeamCompetition
    {
        public int TeamId { get; set; }
        public int CompetitionId { get; set; }

        public Team Team { get; set; }
        public Competition Competition { get; set; }
    }
}
