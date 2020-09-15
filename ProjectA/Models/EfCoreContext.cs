using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectA.Models
{
    public class EfCoreContext : DbContext
    {
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamCompetition>()
                .HasKey(x => new { x.TeamId, x.CompetitionId });

            modelBuilder.Entity<TeamCompetition>()
                .HasOne(x => x.Team)
                .WithMany(x => x.CompetitionsLink)
                .HasForeignKey(x => x.TeamId);

            modelBuilder.Entity<TeamCompetition>()
               .HasOne(x => x.Competition)
               .WithMany(x => x.TeamsLink)
               .HasForeignKey(x => x.CompetitionId);

            base.OnModelCreating(modelBuilder);
        }
    

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Country> Countries { get; set; }

        public EfCoreContext(DbContextOptions<EfCoreContext> options)
                            : base(options) { }

       
    }
}
