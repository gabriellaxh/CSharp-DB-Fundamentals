namespace PhotoShare.Client.Core.Commands
{
    using System;

    using PhotoShare.Data;
    using PhotoShare.Models;
    using System.Linq;

    class LogInCommand
    {
        public static string Execute(string[] data, Session session)
        {
            if (session.IsLoggedIn())
            {
                return $"You should logout first!";
            }

            string username = data[0];
            string password = data[1];

            using (var db = new PhotoShareContext())
            {
                var user = db.Users
                    .FirstOrDefault(x => x.Username == username && x.Password == password);

                if (user == null)
                {
                    throw new ArgumentException("Invalid username or password!!");
                }

                session.Login(user);

                return $"User {username} successfully logged in!";
            }
        }
    }
}
