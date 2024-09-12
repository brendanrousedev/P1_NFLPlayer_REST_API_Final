using System;
using System.Collections.Generic;

namespace P1_NFLPlayer_REST_API_Final.POCO;

public partial class Team
{
    public int TeamId { get; set; }

    public string Name { get; set; } = null!;

    public string Stadium { get; set; } = null!;

    public string City { get; set; } = null!;

    public int Wins { get; set; }

    public int Losses { get; set; }

    public int Ties { get; set; }

    public string? Conference { get; set; }

    public string? Division { get; set; }

    public string Logo { get; set; } = null!;

    public virtual ICollection<Game> GameAwayTeams { get; set; } = new List<Game>();

    public virtual ICollection<Game> GameHomeTeams { get; set; } = new List<Game>();

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();

    public virtual ICollection<TeamStat> TeamStats { get; set; } = new List<TeamStat>();
}
