namespace PhotoShare.Client.Core.Commands
{
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class AcceptFriendCommand
    {
        // AcceptFriend <username1> <username2>
        public static string Execute(string[] data, Session session)
        {
            string friendToAcceptName = data[1];

            using (var db = new PhotoShareContext())
            {
                var user = db.Users
                    .SingleOrDefault(u => u.Id == session.User.Id);

                var friendToAccept = db.Users
                    .Include(x => x.FriendsAdded)
                    .ThenInclude(z => z.Friend)
                    .Where(u => u.Username == friendToAcceptName)
                    .FirstOrDefault();

                if (friendToAccept == null)
                {
                    throw new ArgumentException($"{friendToAcceptName} is not found!");
                }

                bool userFriends = user.FriendsAdded
                    .Any(u => u.Friend.Id == friendToAccept.Id);
                bool friendToAcceptFriends = friendToAccept.FriendsAdded
                    .Any(x => x.Friend.Id == user.Id);

                if(userFriends && friendToAcceptFriends)
                {
                    throw new InvalidOperationException($"{friendToAcceptName} is already a friend to {user.Username}.");
                }

                if(!friendToAcceptFriends && !userFriends)
                {
                    throw new InvalidOperationException($"{friendToAcceptName} has not added {user.Username} as a friend!");
                }

                if(!friendToAcceptFriends && userFriends)
                {
                    throw new InvalidOperationException($"{user.Username} has send Friend request. {friendToAcceptName} need to accept it!");
                }

                db.Users.SingleOrDefault(x => x.Id == user.Id)
                    .FriendsAdded.Add(new Friendship()
                    {
                        Friend = friendToAccept
                    });

                db.SaveChanges();

                return $"{user.Username} accepted {friendToAcceptName} as a friend!";
            }
        }
    }
}
