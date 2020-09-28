using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectA.Models
{
    public class Country
    {
        public Country()
        {
            NationalCompetitions = new HashSet<NationalCompetition>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public Region Region { get; set; }
        public ICollection<NationalCompetition> NationalCompetitions { get; set; }
    }
}
