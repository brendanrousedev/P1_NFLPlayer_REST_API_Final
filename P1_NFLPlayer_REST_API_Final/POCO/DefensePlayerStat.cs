using System;
using System.Collections.Generic;

namespace P1_NFLPlayer_REST_API_Final.POCO;

public partial class DefensePlayerStat
{
    public int StatId { get; set; }

    public int? PlayerId { get; set; }

    public int? GameId { get; set; }

    public int Tackles { get; set; }

    public int Assists { get; set; }

    public double Sacks { get; set; }

    public int Interceptions { get; set; }

    public virtual Game? Game { get; set; }

    public virtual Player? Player { get; set; }
}
