namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class AddTagToCommand 
    {
        // AddTagTo <albumName> <tag>
        public static string Execute(string[] data)
        {
            var albumName = data[1];
            var albumTag = data[2];

            using(var db = new PhotoShareContext())
            {
                var album = db.Albums
                    .Where(x => x.Name == albumName)
                    .SingleOrDefault();

                var tag = db.Tags
                    .Where(x => x.Name == albumTag)
                    .SingleOrDefault();

                if (album == null || tag == null)
                {
                    throw new ArgumentException($"Either tag or album do not exist!");
                }

                var newAlbum = new AlbumTag()
                {
                    Album = album,
                    Tag = tag
                };

                db.AlbumTags.Add(newAlbum);
                db.SaveChanges();

                return $"Tag #{tag} added to {album}!";
            }
        }
    }
}
