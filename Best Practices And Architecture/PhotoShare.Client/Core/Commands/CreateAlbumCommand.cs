namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CreateAlbumCommand
    {
        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public static string Execute(string[] data, Session session)
        {
            var albumTitle = data[1];
            var colorName = data[2];
            string[] tags = data.Skip(3).ToArray();

            var color = new Color();

            var colors = new[]
            {
                "White",
                "Green",
                "Blue",
                "Pink",
                "Yellow",
                "Black",
                "Cyan",
                "Magenta",
                "Red",
                "Purple",
                "WhiteBlackGradient"
            };

            var albumTags = new List<Tag>();

            using (var db = new PhotoShareContext())
            {
                var currentUser = db.Users
                    .SingleOrDefault(x => x.Id == session.User.Id);

                if (session.User == null)
                {
                    throw new ArgumentException($"User {session.User.Username} not found!");
                }

                if (db.Albums.Any(x => x.Name == albumTitle))
                {
                    throw new ArgumentException($"Album {albumTitle} exists!");
                }

                switch (colorName.ToLower())
                {
                    case "white":
                        color = Color.White;
                        break;
                    case "green":
                        color = Color.Green;
                        break;
                    case "blue":
                        color = Color.Blue;
                        break;
                    case "pink":
                        color = Color.Pink;
                        break;
                    case "yellow":
                        color = Color.Yellow;
                        break;
                    case "black":
                        color = Color.Black;
                        break;
                    case "cyan":
                        color = Color.Cyan;
                        break;
                    case "magenta":
                        color = Color.Magenta;
                        break;
                    case "red":
                        color = Color.Red;
                        break;
                    case "purple":
                        color = Color.Purple;
                        break;
                    case "whiteblackgradient":
                        color = Color.WhiteBlackGradient;
                        break;
                    default:
                        throw new ArgumentException($"Color {colorName} not found!" + Environment.NewLine +
                                                    "You may use one of this: " + string.Join(", ", colors));
                }

                foreach (var t in tags)
                {
                    var tag = db.Tags
                        .Where(x => x.Name == t)
                        .SingleOrDefault();

                    if (tag == null)
                    {
                        throw new ArgumentException($"Invalid tags!" + Environment.NewLine + $"{t} not exist." + Environment.NewLine
                           + "Please add any new tags before to use them.");
                    }

                    albumTags.Add(tag);
                }
                var album = new Album()
                {
                    Name = albumTitle,
                    BackgroundColor = color,
                    AlbumRoles = new List<AlbumRole>()
                {
                    new AlbumRole()
                    {
                        User = currentUser,
                        Role = Role.Owner
                    }
                },
                    AlbumTags = albumTags.Select(x => new AlbumTag()
                    {
                        Tag = db.Tags.FirstOrDefault(z => z.Name == x.Name)
                    })
                    .ToArray()
                };

                db.Albums.Add(album);
                db.SaveChanges();

                return $"Album {albumTitle} successfully created!";
            }


        }
    }
}
