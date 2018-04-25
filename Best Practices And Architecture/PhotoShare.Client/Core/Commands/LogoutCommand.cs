using PhotoShare.Models;
using System;

namespace PhotoShare.Client.Core.Commands
{
    class LogoutCommand
    {
        public static string Execute(Session session)
        {
            if (!session.IsLoggedIn())
            {
                throw new InvalidOperationException("You should log in first in order to logout.");
            }

            session.Logout();
            return $"User {session.User.Username} successfully logged out!";
        }
    }
}
