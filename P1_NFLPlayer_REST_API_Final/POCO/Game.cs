using System;
using System.Collections.Generic;

namespace P1_NFLPlayer_REST_API_Final.POCO;

public partial class Game
{
    public int GameId { get; set; }

    public int? HomeTeamId { get; set; }

    public int? AwayTeamId { get; set; }

    public int HomeTeamScore { get; set; }

    public int AwayTeamScore { get; set; }

    public virtual Team? AwayTeam { get; set; }

    public virtual ICollection<DefensePlayerStat> DefensePlayerStats { get; set; } = new List<DefensePlayerStat>();

    public virtual Team? HomeTeam { get; set; }

    public virtual ICollection<KickReturnStat> KickReturnStats { get; set; } = new List<KickReturnStat>();

    public virtual ICollection<KickingStat> KickingStats { get; set; } = new List<KickingStat>();

    public virtual ICollection<PassingPlayerStat> PassingPlayerStats { get; set; } = new List<PassingPlayerStat>();

    public virtual ICollection<PuntReturnStat> PuntReturnStats { get; set; } = new List<PuntReturnStat>();

    public virtual ICollection<PuntingStat> PuntingStats { get; set; } = new List<PuntingStat>();

    public virtual ICollection<ReceivingPlayerStat> ReceivingPlayerStats { get; set; } = new List<ReceivingPlayerStat>();

    public virtual ICollection<RushingPlayerStat> RushingPlayerStats { get; set; } = new List<RushingPlayerStat>();

    public virtual ICollection<TeamStat> TeamStats { get; set; } = new List<TeamStat>();
}
