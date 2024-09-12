using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using P1_NFLPlayer_REST_API_Final.POCO;

namespace P1_NFLPlayer_REST_API_Final.POCO;

public partial class Player
{
    public int PlayerId { get; set; }

    public string Name { get; set; } = null!;

    public int? TeamId { get; set; }

    public int? Number { get; set; }

    public string? PictureUrl { get; set; }

    public virtual ICollection<DefensePlayerStat> DefensePlayerStats { get; set; } = new List<DefensePlayerStat>();

    public virtual ICollection<KickReturnStat> KickReturnStats { get; set; } = new List<KickReturnStat>();

    public virtual ICollection<KickingStat> KickingStats { get; set; } = new List<KickingStat>();

    public virtual ICollection<PassingPlayerStat> PassingPlayerStats { get; set; } = new List<PassingPlayerStat>();

    public virtual ICollection<PuntReturnStat> PuntReturnStats { get; set; } = new List<PuntReturnStat>();

    public virtual ICollection<PuntingStat> PuntingStats { get; set; } = new List<PuntingStat>();

    public virtual ICollection<ReceivingPlayerStat> ReceivingPlayerStats { get; set; } = new List<ReceivingPlayerStat>();

    public virtual ICollection<RushingPlayerStat> RushingPlayerStats { get; set; } = new List<RushingPlayerStat>();

    public virtual Team? Team { get; set; }
}
