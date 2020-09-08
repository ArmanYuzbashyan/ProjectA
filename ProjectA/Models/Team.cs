using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectA.Models
{
    public class Team
    {
        public Guid TeamId { get; set; }
        public string TeamName { get; set; }
        public string ManagerName { get; set; }
        public Country TeamCountry { get; set; }        
    }
}
