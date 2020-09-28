using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectA.Models
{
    public class Region
    {
        public Region()
        {
            GlobalCompetitions = new HashSet<GlobalCompetition>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<GlobalCompetition> GlobalCompetitions { get; set; }
    }
}
