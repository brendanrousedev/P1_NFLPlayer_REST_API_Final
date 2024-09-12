using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace P1_NFLPlayer_REST_API_Final.POCO;

public partial class Nfl2024playerDbContext : DbContext
{
    public Nfl2024playerDbContext()
    {
    }

    public Nfl2024playerDbContext(DbContextOptions<Nfl2024playerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DefensePlayerStat> DefensePlayerStats { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<KickReturnStat> KickReturnStats { get; set; }

    public virtual DbSet<KickingStat> KickingStats { get; set; }

    public virtual DbSet<PassingLeader> PassingLeaders { get; set; }

    public virtual DbSet<PassingPlayerStat> PassingPlayerStats { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PuntReturnStat> PuntReturnStats { get; set; }

    public virtual DbSet<PuntingStat> PuntingStats { get; set; }

    public virtual DbSet<ReceivingLeader> ReceivingLeaders { get; set; }

    public virtual DbSet<ReceivingPlayerStat> ReceivingPlayerStats { get; set; }

    public virtual DbSet<RushingLeader> RushingLeaders { get; set; }

    public virtual DbSet<RushingPlayerStat> RushingPlayerStats { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamStat> TeamStats { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=localhost;database=NFL2024PlayerDB;integrated security=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefensePlayerStat>(entity =>
        {
            entity.HasKey(e => e.StatId).HasName("PK__DefenseP__3A162D3E78A5A578");

            entity.ToTable(tb => tb.HasTrigger("trg_UpdateTeamStatsOnDefensePlayerStatsInsert"));

            entity.HasIndex(e => new { e.PlayerId, e.GameId }, "UC_DefenseStats").IsUnique();

            entity.HasOne(d => d.Game).WithMany(p => p.DefensePlayerStats)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK__DefensePl__GameI__619B8048");

            entity.HasOne(d => d.Player).WithMany(p => p.DefensePlayerStats)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK__DefensePl__Playe__60A75C0F");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.GameId).HasName("PK__Game__2AB897FD0EEED083");

            entity.ToTable("Game", tb =>
                {
                    tb.HasTrigger("trg_CreateTeamStatsOnGameInsert");
                    tb.HasTrigger("trg_UpdateTeamStatsOnGameInsert");
                });

            entity.HasOne(d => d.AwayTeam).WithMany(p => p.GameAwayTeams)
                .HasForeignKey(d => d.AwayTeamId)
                .HasConstraintName("FK__Game__AwayTeamId__3F466844");

            entity.HasOne(d => d.HomeTeam).WithMany(p => p.GameHomeTeams)
                .HasForeignKey(d => d.HomeTeamId)
                .HasConstraintName("FK__Game__HomeTeamId__3E52440B");
        });

        modelBuilder.Entity<KickReturnStat>(entity =>
        {
            entity.HasKey(e => e.StatId).HasName("PK__KickRetu__3A162D3E76541AFD");

            entity.HasIndex(e => new { e.PlayerId, e.GameId }, "UC_KickReturnStats").IsUnique();

            entity.Property(e => e.AverageYpc)
                .HasComputedColumnSql("(CONVERT([decimal](5,2),[Yards])/nullif([Returns],(0)))", false)
                .HasColumnType("decimal(16, 13)")
                .HasColumnName("AverageYPC");

            entity.HasOne(d => d.Game).WithMany(p => p.KickReturnStats)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK__KickRetur__GameI__68487DD7");

            entity.HasOne(d => d.Player).WithMany(p => p.KickReturnStats)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK__KickRetur__Playe__6754599E");
        });

        modelBuilder.Entity<KickingStat>(entity =>
        {
            entity.HasKey(e => e.StatId).HasName("PK__KickingS__3A162D3E5F22FCE7");

            entity.ToTable(tb => tb.HasTrigger("trg_UpdateTeamStatsOnKickingStatsInsert"));

            entity.HasIndex(e => new { e.PlayerId, e.GameId }, "UC_KickingStats").IsUnique();

            entity.Property(e => e.Percentage)
                .HasComputedColumnSql("(CONVERT([decimal](5,2),[Attempts])/nullif([KicksMade],(0)))", false)
                .HasColumnType("decimal(16, 13)");

            entity.HasOne(d => d.Game).WithMany(p => p.KickingStats)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK__KickingSt__GameI__75A278F5");

            entity.HasOne(d => d.Player).WithMany(p => p.KickingStats)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK__KickingSt__Playe__74AE54BC");
        });

        modelBuilder.Entity<PassingLeader>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("PassingLeaders");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PassingPlayerStat>(entity =>
        {
            entity.HasKey(e => e.StatId).HasName("PK__PassingP__3A162D3EBF546AB4");

            entity.ToTable(tb => tb.HasTrigger("trg_UpdateTeamStatsOnPassingPlayerStatsInsert"));

            entity.HasIndex(e => new { e.PlayerId, e.GameId }, "UC_PlayerGame").IsUnique();

            entity.Property(e => e.AverageYpc)
                .HasComputedColumnSql("(CONVERT([decimal](5,2),[Yards])/nullif([Attempts],(0)))", false)
                .HasColumnType("decimal(16, 13)")
                .HasColumnName("AverageYPC");
            entity.Property(e => e.CompletionPercentage)
                .HasComputedColumnSql("(CONVERT([decimal](5,2),[Completions])/nullif([Attempts],(0)))", false)
                .HasColumnType("decimal(16, 13)");

            entity.HasOne(d => d.Game).WithMany(p => p.PassingPlayerStats)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK__PassingPl__Inter__4CA06362");

            entity.HasOne(d => d.Player).WithMany(p => p.PassingPlayerStats)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK__PassingPl__Playe__46E78A0C");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.PlayerId).HasName("PK__Player__4A4E74C8B958BCCB");

            entity.ToTable("Player");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PictureUrl)
                .IsUnicode(false)
                .HasColumnName("PictureURL");

            entity.HasOne(d => d.Team).WithMany(p => p.Players)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__Player__TeamId__440B1D61");
        });

        modelBuilder.Entity<PuntReturnStat>(entity =>
        {
            entity.HasKey(e => e.StatId).HasName("PK__PuntRetu__3A162D3EB8471CD9");

            entity.HasIndex(e => new { e.PlayerId, e.GameId }, "UC_PuntReturnStats").IsUnique();

            entity.Property(e => e.AverageYpc)
                .HasComputedColumnSql("(CONVERT([decimal](5,2),[Yards])/nullif([Returns],(0)))", false)
                .HasColumnType("decimal(16, 13)")
                .HasColumnName("AverageYPC");

            entity.HasOne(d => d.Game).WithMany(p => p.PuntReturnStats)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK__PuntRetur__GameI__6EF57B66");

            entity.HasOne(d => d.Player).WithMany(p => p.PuntReturnStats)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK__PuntRetur__Playe__6E01572D");
        });

        modelBuilder.Entity<PuntingStat>(entity =>
        {
            entity.HasKey(e => e.StatId).HasName("PK__PuntingS__3A162D3E807D65DF");

            entity.ToTable(tb => tb.HasTrigger("trg_UpdateTeamStatsOnPuntingStatsInsert"));

            entity.HasIndex(e => new { e.PlayerId, e.GameId }, "UC_PuntingStats").IsUnique();

            entity.Property(e => e.Average)
                .HasComputedColumnSql("(CONVERT([decimal](5,2),[Yards])/nullif([Punts],(0)))", false)
                .HasColumnType("decimal(16, 13)");

            entity.HasOne(d => d.Game).WithMany(p => p.PuntingStats)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK__PuntingSt__GameI__7C4F7684");

            entity.HasOne(d => d.Player).WithMany(p => p.PuntingStats)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK__PuntingSt__Playe__7B5B524B");
        });

        modelBuilder.Entity<ReceivingLeader>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ReceivingLeaders");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ReceivingPlayerStat>(entity =>
        {
            entity.HasKey(e => e.StatId).HasName("PK__Receivin__3A162D3ED372D995");

            entity.HasIndex(e => new { e.PlayerId, e.GameId }, "UC_ReceivingGame").IsUnique();

            entity.Property(e => e.AverageYpc)
                .HasComputedColumnSql("(CONVERT([decimal](5,2),[Yards])/nullif([Receptions],(0)))", false)
                .HasColumnType("decimal(16, 13)")
                .HasColumnName("AverageYPC");

            entity.HasOne(d => d.Game).WithMany(p => p.ReceivingPlayerStats)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK__Receiving__GameI__59FA5E80");

            entity.HasOne(d => d.Player).WithMany(p => p.ReceivingPlayerStats)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK__Receiving__Playe__59063A47");
        });

        modelBuilder.Entity<RushingLeader>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("RushingLeaders");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RushingPlayerStat>(entity =>
        {
            entity.HasKey(e => e.StatId).HasName("PK__RushingP__3A162D3EBE002E96");

            entity.ToTable(tb => tb.HasTrigger("trg_UpdateTeamStatsOnRushingPlayerStatsInsert"));

            entity.HasIndex(e => new { e.PlayerId, e.GameId }, "UC_RushingGame").IsUnique();

            entity.Property(e => e.AverageYpc)
                .HasComputedColumnSql("(CONVERT([decimal](5,2),[Yards])/nullif([Carries],(0)))", false)
                .HasColumnType("decimal(16, 13)")
                .HasColumnName("AverageYPC");

            entity.HasOne(d => d.Game).WithMany(p => p.RushingPlayerStats)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK__RushingPl__GameI__534D60F1");

            entity.HasOne(d => d.Player).WithMany(p => p.RushingPlayerStats)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK__RushingPl__Touch__52593CB8");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("PK__Team__123AE79957D91B57");

            entity.ToTable("Team");

            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Conference)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Division)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Logo).IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Stadium)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TeamStat>(entity =>
        {
            entity.HasKey(e => e.StatId).HasName("PK__TeamStat__3A162D3EA76242E4");

            entity.HasIndex(e => new { e.TeamId, e.GameId }, "UC_TeamStats").IsUnique();

            entity.Property(e => e.TotalYards).HasComputedColumnSql("([PassingYards]+[RushingYards])", false);
            entity.Property(e => e.Ypp)
                .HasComputedColumnSql("(CONVERT([decimal](5,2),[PassingYards]+[RushingYards])/nullif([Plays],(0)))", false)
                .HasColumnType("decimal(16, 13)")
                .HasColumnName("YPP");

            entity.HasOne(d => d.Game).WithMany(p => p.TeamStats)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK__TeamStats__GameI__06CD04F7");

            entity.HasOne(d => d.Team).WithMany(p => p.TeamStats)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__TeamStats__TeamI__05D8E0BE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
