﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ProjectA.DTO;
using ProjectA.Actions.Abstraction;

namespace ProjectA.Actions
{
    public  class PlayerLogic : IPlayerLogic
    {
        private readonly EfCoreContext _context;

        public PlayerLogic(EfCoreContext context)
        {
            _context = context;
        }        
        public async Task<ActionResult<IEnumerable<Player>>> GetAll()
        {
            var Players = _context.Players
                .Include(n => n.Nation)
                .Include(t => t.Team)
                .ToListAsync();
            return await Players;
        }
        public async Task<bool> Add(PlayerDto playerDto)
        {
            if (string.IsNullOrWhiteSpace(playerDto.PlayerName)
                || string.IsNullOrWhiteSpace(playerDto.Position))
            {
                return false;
            }
            var playerNation = await _context.Countries.FindAsync(playerDto.NationId);
            var playerTeam = await _context.Teams.FindAsync(playerDto.TeamId);
            if (playerNation == null || playerTeam == null)
            {
                return false;
            }
            var player = new Player
            {
                Name = playerDto.PlayerName,
                Position = playerDto.Position,
                Team = playerTeam,
                Nation = playerNation
            };

            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Edit(int id, PlayerDto playerDto)
        {
            if (string.IsNullOrWhiteSpace(playerDto.PlayerName)
                || string.IsNullOrWhiteSpace(playerDto.Position))
            {
                return false;
            }
            var playerNation = await _context.Countries.FindAsync(playerDto.NationId);
            var playerTeam = await _context.Teams.FindAsync(playerDto.TeamId);
            if (playerNation == null || playerTeam == null)
            {
                return false;
            }
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return false;
            }
            player.Name = playerDto.PlayerName;
            player.Position = playerDto.Position;
            player.Team = playerTeam;
            player.Nation = playerNation;
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
