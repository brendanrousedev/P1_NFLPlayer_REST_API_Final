using System;
using System.Collections.Generic;

namespace P1_NFLPlayer_REST_API_Final.POCO;

public partial class PassingPlayerStat
{
    public int StatId { get; set; }

    public int? PlayerId { get; set; }

    public int? GameId { get; set; }

    public int Completions { get; set; }

    public int Attempts { get; set; }

    public int Yards { get; set; }

    public decimal? CompletionPercentage { get; set; }

    public decimal? AverageYpc { get; set; }

    public int Touchdowns { get; set; }

    public int Interceptions { get; set; }

    public virtual Game? Game { get; set; }

    public virtual Player? Player { get; set; }
}
