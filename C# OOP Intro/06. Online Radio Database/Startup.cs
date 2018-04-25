using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Startup
{
    static void Main(string[] args)
    {
        var num = int.Parse(Console.ReadLine());
        var playlist = new List<Song>();
        for (int i = 0; i < num; i++)
        {
            try
            {
                var info = Console.ReadLine().Split(';');
                var songLen = info[2].Split(':');
                int minutes;
                int seconds;
                
                if (songLen.Length != 2 || !int.TryParse((songLen[0]), out minutes) || !int.TryParse((songLen[1]), out seconds))
                {
                    throw new ArgumentException("Invalid song length.");
                }
                playlist.Add(new Song(info[0], info[1], minutes, seconds));
                Console.WriteLine("Song added.");
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

        var totalLenInSecs = (playlist.Select(x => x.Minutes).Sum() * 60) + (playlist.Select(x => x.Seconds).Sum());

        Console.WriteLine($"Songs added: {playlist.Count}");
        Console.WriteLine($"Playlist length: {totalLenInSecs / 3600}h {totalLenInSecs / 60 % 60}m {totalLenInSecs % 60}s");

    }

}


