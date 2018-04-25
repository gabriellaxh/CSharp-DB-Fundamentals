using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Player
{
    private string name { get; set; }
    private int endurance { get; set; }
    private int sprint { get; set; }
    private int dribble { get; set; }
    private int passing { get; set; }
    private int shooting { get; set; }

    public Player(string name, int endurance, int sprint,int dribble,int passing,int shooting)
    {
        this.Name = name;
        this.Endurance = endurance;
        this.Sprint = sprint;
        this.Dribble = dribble;
        this.Passing = passing;
        this.Shooting = shooting;
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

   public int Endurance
    {
        get
        {
            return this.endurance;
        }
        private set
        {
            if(value < 0 || value > 100)
            {
                throw new ArgumentException("Endurance should be between 0 and 100.");
            }
            this.endurance = value;
        }
    }

    public int Sprint
    {
        get
        {
            return this.sprint;
        }
        private set
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentException("Sprint should be between 0 and 100");
            }
            this.sprint = value;
        }
    }

    public int Dribble
    {
        get
        {
            return this.dribble;
        }
        private set
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentException("Dribble should be between 0 and 100");
            }
            this.dribble = value;
        }
    }

    public int Passing
    {
        get
        {
            return this.passing;
        }
        private set
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentException("Passing should be between 0 and 100");
            }
            this.passing = value;
        }
    }

    public int Shooting
    {
        get
        {
            return this.shooting;
        }
        private set
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentException("Shooting should be between 0 and 100");
            }
            this.shooting = value;
        }
    }

    public int SkillLevel()
    {
        return (int)Math.Round((Endurance + Dribble + Sprint + Passing + Shooting) / 5.00);
    }
}

