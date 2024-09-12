using System;
using System.Collections.Generic;

namespace P1_NFLPlayer_REST_API_Final.POCO;

public partial class ReceivingLeader
{
    public int PlayerId { get; set; }

    public string Name { get; set; } = null!;

    public int? TotalReceivingYards { get; set; }

    public int? TotalReceptions { get; set; }

    public int? TotalTouchdowns { get; set; }
}
