namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;
    using System.Text;

    public class PrintFriendsListCommand 
    {
        // PrintFriendsList <username>
        public static string Execute(Session session)
        {
             using(var db = new PhotoShareContext())
            {
                var userFriends = db.Friendships
                    .Where(x => x.UserId == session.User.Id)
                    .ToArray();

                if(session.User == null)
                {
                    throw new ArgumentException($"User {session.User.Username} not found!");
                }

                var builder = new StringBuilder();

                if(userFriends.Length == 0)
                {
                    builder.AppendLine($"No friends for this user. :(");
                }
                else
                {
                    builder.AppendLine("Friends:");
                    foreach (var friend in userFriends)
                    {
                        builder.AppendLine($"-[{friend}]");
                    }
                }

                return builder.ToString().Trim();
            }
        }
    }
}
