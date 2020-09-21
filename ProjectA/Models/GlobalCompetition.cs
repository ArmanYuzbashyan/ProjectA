using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectA.Models
{
    public class GlobalCompetition
    {
        public GlobalCompetition()
        {
            TeamsLink = new HashSet<TeamGlobalCompetition>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public Region Region { get; set; }
        public ICollection<TeamGlobalCompetition> TeamsLink { get; set; }
    }
}
