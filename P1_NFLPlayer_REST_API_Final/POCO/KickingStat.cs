using System;
using System.Collections.Generic;

namespace P1_NFLPlayer_REST_API_Final.POCO;

public partial class KickingStat
{
    public int StatId { get; set; }

    public int? PlayerId { get; set; }

    public int? GameId { get; set; }

    public int Attempts { get; set; }

    public int KicksMade { get; set; }

    public decimal? Percentage { get; set; }

    public int? Long { get; set; }

    public int Points { get; set; }

    public virtual Game? Game { get; set; }

    public virtual Player? Player { get; set; }
}
