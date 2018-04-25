namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using Data;
    using System.Linq;
    using System;

    public class AddTownCommand
    {
        // AddTown <townName> <countryName>
        public static string Execute(string[] data)
        {
            string townName = data[1];
            string country = data[2];

            using (var db = new PhotoShareContext())
            {
               if(db.Towns.Any(x => x.Name == townName))
                {
                    throw new InvalidOperationException($"Town {townName} was already added!");
                }

                Town town = new Town
                {
                    Name = townName,
                    Country = country
                };

                db.Towns.Add(town);
                db.SaveChanges();

                return townName + " was added to database!";
            }
        }
    }
}
