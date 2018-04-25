namespace P03_FootballBetting.Data.Models
{
    using P03_FootballBetting.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Color
    {
        public Color()
        {
            PrimaryKitColorTeams = new List<Team>();
            SecondaryKitColorTeams = new List<Team>();
        }
        public int ColorId { get; set; }
        public string Name { get; set; }

        public ICollection<Team> PrimaryKitColorTeams { get; set; }
        public ICollection<Team> SecondaryKitColorTeams { get; set; }
    }
}
