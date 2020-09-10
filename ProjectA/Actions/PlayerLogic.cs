using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace ProjectA.Actions
{
    public  class PlayerLogic
    {
        private readonly EfCoreContext _context;

        public PlayerLogic(EfCoreContext context)
        {
            _context = context;
        }        
        public async Task<ActionResult<IEnumerable<Player>>> Get()
        {
            var Players = _context.Players.Include(n => n.Nation).ToListAsync();
            return await Players;
        }
        public async Task<bool> Post(Player player)//// always creating new country
        {
            if (player.Nation == null || player.PlayerName == null || player.Position == null)
            {
                return false;
            }
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return false;
            }
            _context.Players.Remove(player);

            await _context.SaveChangesAsync();
            return true;
        }

    }
}
