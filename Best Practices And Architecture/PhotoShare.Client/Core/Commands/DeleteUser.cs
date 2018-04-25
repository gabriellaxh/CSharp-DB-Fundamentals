namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    using Data;
    using PhotoShare.Models;

    public class DeleteUser
    {
        // DeleteUser <username>
        public static string Execute(Session session)
        {
            using (var db = new PhotoShareContext())
            {
                db.Users.SingleOrDefault(x => x.Id == session.User.Id).IsDeleted = true;
                               
                db.SaveChanges();

                return $"User {session.User.Username} was deleted from the database!";
            }
        }
    }
}
