using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ProjectA.Models
{
    public class EfCoreContext : DbContext
    {
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamNationalCompetition>()
                .HasKey(x => new { x.TeamId, x.CompetitionId });
            modelBuilder.Entity<TeamGlobalCompetition>()
                .HasKey(y => new { y.TeamId, y.CompetitionId });

            //modelBuilder.Entity<TeamCompetition>()
            //    .HasOne(x => x.Team)
            //    .WithMany(x => x.CompetitionsLink)
            //    .HasForeignKey(x => x.TeamId);

            //modelBuilder.Entity<TeamCompetition>()
            //   .HasOne(x => x.Competition)
            //   .WithMany(x => x.TeamsLink)
            //   .HasForeignKey(x => x.CompetitionId);

            base.OnModelCreating(modelBuilder);
        }
    

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<NationalCompetition> NationalCompetitions { get; set; }
        public DbSet<GlobalCompetition> GlobalCompetitions { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }


        // public DbSet<TeamCompetition> TeamCompetitions { get; set; }
        public EfCoreContext(DbContextOptions<EfCoreContext> options)
                            : base(options) { }

       
    }
}
