using FanDuelSolution.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FanDuelSolution.API.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected AppDbContext()
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<DepthChart> DepthCharts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().HasData(
                new Team("Tampa Bay Buccaneers") { Id = 1, Abbreviation = "TBB" }
                );

            modelBuilder.Entity<Position>().HasData(
                new Position("LWR", "Offense") { Id = 1 },
                new Position("RWR", "Offense") { Id = 2 },
                new Position("LT", "Offense") { Id = 3 },
                new Position("LG", "Offense") { Id = 4 },
                new Position("C", "Offense") { Id = 5 },
                new Position("RG", "Offense") { Id = 6 },
                new Position("RT", "Offense") { Id = 7 },
                new Position("TE", "Offense") { Id = 8 },
                new Position("QB", "Offense") { Id = 9 },
                new Position("RB", "Offense") { Id = 10 },
                new Position("DE", "Defense") { Id = 11 },
                new Position("NT", "Defense") { Id = 12 },
                new Position("OLB", "Defense") { Id = 13 },
                new Position("ILB", "Defense") { Id = 14 },
                new Position("CB", "Defense") { Id = 15 },
                new Position("SS", "Defense") { Id = 16 },
                new Position("FS", "Defense") { Id = 17 },
                new Position("RCB", "Defense") { Id = 18 },
                new Position("PT", "Special Teams") { Id = 19 },
                new Position("PK", "Special Teams") { Id = 20 },
                new Position("LS", "Special Teams") { Id = 21 },
                new Position("H", "Special Teams") { Id = 22 },
                new Position("KO", "Special Teams") { Id = 23 },
                new Position("PR", "Special Teams") { Id = 24 },
                new Position("KR", "Special Teams") { Id = 25 },
                new Position("RES", "Reserves") { Id = 26 },
                new Position("FUT", "Reserves") { Id = 27 }
                );

            modelBuilder.Entity<Player>().HasData(
                new Player("Mike", "Evans") { Id = 1, Number = 13 },
                new Player("Tyler", "Johnson") { Id = 2, Number = 18 },
                new Player("Donovan", "Smith") { Id = 3, Number = 76 },
                new Player("Ali", "Marpet") { Id = 4, Number = 74 },
                new Player("Ryan", "Jensen") { Id = 5, Number = 66 },
                new Player("Alex", "Cappa") { Id = 6, Number = 65 },
                new Player("Tristan", "Wirfs") { Id = 7, Number = 78 },
                new Player("Jaelon", "Darden") { Id = 8, Number = 1 },
                new Player("Scott", "Miller") { Id = 9, Number = 10 },
                new Player("Breshad", "Perriman") { Id = 10, Number = 16 },
                new Player("Cyril", "Grayson") { Id = 11, Number = 15 }
                );

            modelBuilder.Entity<DepthChart>().HasData(
                // Mike Evans
                new DepthChart { Id = 1, TeamId = 1, PositionId = 1, PlayerId = 1, PositionDepth = 1 },
                // Jaelon Darden
                new DepthChart { Id = 2, TeamId = 1, PositionId = 1, PlayerId = 8, PositionDepth = 2 },
                // Breshad Perriman
                new DepthChart { Id = 3, TeamId = 1, PositionId = 2, PlayerId = 10, PositionDepth = 1 },
                // Ciryl Grayson
                new DepthChart { Id = 4, TeamId = 1, PositionId = 2, PlayerId = 11, PositionDepth = 2 }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
