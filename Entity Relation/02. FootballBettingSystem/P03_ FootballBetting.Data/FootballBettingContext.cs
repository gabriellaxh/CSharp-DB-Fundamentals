namespace P03_FootballBetting.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using P03_FootballBetting.Data.Models;
    using P03_FootballBetting.Data;

    public class FootballBettingContext : DbContext
    {
        public FootballBettingContext()
        { }

        public FootballBettingContext(DbContextOptions options)
            :base(options)
        { }

        public DbSet<Player> Players { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(e => e.PlayerId);

                entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(e => e.IsInjured)
                .HasDefaultValue(false);


                entity.HasOne(p => p.Position)
                .WithMany(p => p.Players)
                .HasForeignKey(p => p.PositionId);

                entity.HasOne(e => e.Team)
                .WithMany(p => p.Players)
                .HasForeignKey(e => e.TeamId);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.TeamId);

                entity.Property(e => e.Initials)
                .HasColumnType("NCHAR(3)")
                .IsRequired();

                entity.HasOne(e => e.PrimaryKitColor)
                .WithMany(x => x.PrimaryKitColorTeams)
                .HasForeignKey(e => e.PrimaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.SecondaryKitColor)
                .WithMany(e => e.SecondaryKitColorTeams)
                .HasForeignKey(e => e.SecondaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Town)
                .WithMany(e => e.Teams)
                .HasForeignKey(e => e.TownId);
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.HasKey(e => e.ColorId);

                entity.Property(e => e.Name)
                .IsRequired();

            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.Username)
                .IsRequired();
                
                entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(e => e.Balance)
                .HasColumnType("DECIMAL(20,5");
            });

            modelBuilder.Entity<Bet>(entity =>
            {
                entity.HasKey(e => e.BetId);

                entity.HasOne(e => e.User)
                .WithMany(x => x.Bets)
                .HasForeignKey(e => e.UserId);

                entity.HasOne(e => e.Game)
                .WithMany(e => e.Bets)
                .HasForeignKey(e => e.GameId);

            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.CountryId);

                entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);                
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasKey(e => e.GameId);

                entity.HasOne(p => p.HomeTeam)
                .WithMany(p => p.HomeGames)
                .HasForeignKey(p => p.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.AwayTeam)
                .WithMany(p => p.AwayGames)
                .HasForeignKey(e => e.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<PlayerStatistic>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.PlayerId,
                    e.GameId
                });

              entity.HasOne(p => p.Game)
                .WithMany(p => p.PlayerStatistics)
                .HasForeignKey(p => p.GameId);

                entity.HasOne(e => e.Player)
                .WithMany(p => p.PlayerStatistics)
                .HasForeignKey(e => e.PlayerId);
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(e => e.PositionId);

                entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            });
        }
    }
}
