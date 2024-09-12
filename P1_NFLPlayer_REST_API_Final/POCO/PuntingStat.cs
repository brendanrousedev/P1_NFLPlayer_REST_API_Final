using System;
using System.Collections.Generic;

namespace P1_NFLPlayer_REST_API_Final.POCO;

public partial class PuntingStat
{
    public int StatId { get; set; }

    public int? PlayerId { get; set; }

    public int? GameId { get; set; }

    public int Punts { get; set; }

    public int Yards { get; set; }

    public decimal? Average { get; set; }

    public int In20 { get; set; }

    public virtual Game? Game { get; set; }

    public virtual Player? Player { get; set; }
}
