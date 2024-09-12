using System;
using System.Collections.Generic;

namespace P1_NFLPlayer_REST_API_Final.POCO;

public partial class RushingLeader
{
    public int PlayerId { get; set; }

    public string Name { get; set; } = null!;

    public int? TotalRushingYards { get; set; }

    public int? TotalCarries { get; set; }

    public int? TotalTouchdowns { get; set; }
}
