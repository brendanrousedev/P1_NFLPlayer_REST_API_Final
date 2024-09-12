using System;
using System.Collections.Generic;

namespace P1_NFLPlayer_REST_API_Final.POCO;

public partial class TeamStat
{
    public int StatId { get; set; }

    public int? TeamId { get; set; }

    public int? GameId { get; set; }

    public int PassingYards { get; set; }

    public int RushingYards { get; set; }

    public int? TotalYards { get; set; }

    public int Plays { get; set; }

    public decimal? Ypp { get; set; }

    public int FirstDowns { get; set; }

    public int SacksAllowed { get; set; }

    public int Punts { get; set; }

    public int IntThrown { get; set; }

    public TimeOnly? TimeofPossession { get; set; }

    public virtual Game? Game { get; set; }

    public virtual Team? Team { get; set; }
}
