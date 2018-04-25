using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Startup
{
    static void Main(string[] args)
    {

        var teams = new Dictionary<string, Team>();
        while (true)
        {

            try
            {
                var info = Console.ReadLine().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (info[0] == "END")

                    break;

                if (info[0] == "Team")
                {
                    var team = new Team(info[1]);
                    teams.Add(info[1], team);
                }
                else if (info[0] == "Add")
                {
                    if (!teams.ContainsKey(info[1]))
                    {
                        throw new ArgumentException($"Team {info[1]} does not exist.");
                    }
                    var player = new Player(info[1],
                                            int.Parse(info[3]),
                                            int.Parse(info[4]),
                                            int.Parse(info[5]),
                                            int.Parse(info[6]),
                                            int.Parse(info[7]));
                    teams[info[1]].AddPlayer(player);
                }
                else if (info[0] == "Remove")
                {
                    if (!teams.ContainsKey(info[1]))
                    {
                        throw new ArgumentException($"Team {info[1]} does not exist.");
                    }

                    else
                    {
                        teams[info[1]].RemovePlayer(info[2]);
                    }
                }
                else if (info[0] == "Rating")
                {
                    if (!teams.ContainsKey(info[1]))
                    {
                        throw new ArgumentException($"Team {info[1]} does not exist.");
                    }
                    Console.WriteLine($"{teams[info[1]].Name} - {teams[info[1]].Rating}");
                }

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }


    }
}

