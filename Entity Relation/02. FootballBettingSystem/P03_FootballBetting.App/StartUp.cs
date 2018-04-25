using P03_FootballBetting.Data;
using System;

namespace P03_FootballBetting.Data
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var db = new FootballBettingContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }
    }
}
