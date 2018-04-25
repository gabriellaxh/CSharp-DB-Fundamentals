namespace PhotoShare.Client.Core.Commands
{
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class AddFriendCommand
    {
        // AddFriend <username1> <username2>
        public static string Execute(string[] data, Session session)
        {
            string friendToAddName = data[1];

            using (var db = new PhotoShareContext())
            {
                var friendToAdd = db.Users
                   .Include(x => x.FriendsAdded)
                   .ThenInclude(x => x.Friend)
                   .FirstOrDefault(x => x.Username == friendToAddName);

                if (friendToAdd == null)
                {
                    throw new ArgumentException($"{friendToAddName} not found!");
                }

                if (friendToAdd.Id == session.User.Id)
                {
                    throw new ArgumentException("You can't give friendship to yourself :(");
                }

                bool userFriend = session.User.FriendsAdded
                    .Any(x => x.Friend.Id == friendToAdd.Id);
                bool friendFriends = friendToAdd.FriendsAdded
                    .Any(x => x.Friend.Id == session.User.Id);


                if (userFriend && friendFriends)
                {
                    throw new InvalidOperationException($"{friendToAddName} is already a friend to {session.User.Username}");
                }

                if (!userFriend && friendFriends)
                {
                    throw new InvalidOperationException($"{friendToAddName} has already send you friend request, You need to accept it!");
                }

                if (userFriend && !friendFriends)
                {
                    throw new InvalidOperationException($"Friend request already sended to {friendToAddName}");
                }

                db.Users.SingleOrDefault(x => x.Username == session.User.Username)
                    .FriendsAdded.Add(new Friendship
                    {
                        Friend = friendToAdd
                    });

                db.SaveChanges();

                return $"Friend {friendToAddName} added to {session.User.Username}";
            }
        }
    }
}
