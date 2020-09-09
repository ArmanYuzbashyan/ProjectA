using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectA.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public Country Nation { get; set; }
        public string Position { get; set; }
        public Team Team { get; set; }
    }
}
