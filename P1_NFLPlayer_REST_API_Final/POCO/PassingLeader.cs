using System;
using System.Collections.Generic;

namespace P1_NFLPlayer_REST_API_Final.POCO;

public partial class PassingLeader
{
    public int PlayerId { get; set; }

    public string Name { get; set; } = null!;

    public int? TotalPassingYards { get; set; }

    public int? TotalCompletions { get; set; }

    public int? TotalAttempts { get; set; }

    public int? TotalTouchdowns { get; set; }

    public int? TotalInterceptions { get; set; }
}
