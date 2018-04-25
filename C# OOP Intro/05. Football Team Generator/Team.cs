using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Team
{
    private Dictionary<string, Player> players;
    private string name { get; set; }
    private int rating = 0;

    public Team(string name)
    {
        this.PLayers = new Dictionary<string, Player>();
        this.Name = name;
    }

    public string Name
    {
        get
        {
            return this.name;
        }
        set
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("A name should not be empty.");
            }
            this.name = value;
        }
    }
    public int Rating
    {
        get
        {
            return this.rating;
        }
        set
        {
            this.rating = value;
        }
    }
    private Dictionary<string, Player> PLayers
    {
        get
        {
           return this.players;
        }
        set
        {
            this.players = value;
        }
    }

    private int CalcRating()
    {
        if (PLayers.Count == 0)
        {
            return 0;
        }
        return (int)Math.Round(PLayers.Sum(x => x.Value.SkillLevel()) / (double)PLayers.Count());
    }

    public void AddPlayer(Player player)
    {
        this.players.Add(player.Name, player);
        Rating = this.CalcRating();
    }

    public void RemovePlayer(string player)
    {
        if (!PLayers.ContainsKey(player))
        {
            throw new ArgumentException($"Player {player} is not in {Name} team.");
        }
        else
        {
            
            this.PLayers.Remove(player);
            Rating = this.CalcRating();
        }

    }
}

