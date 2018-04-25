namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class UploadPictureCommand
    {
        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public static string Execute(string[] data)
        {
            var albumName = data[1];
            var picTitle = data[2];
            var path = data[3];

            using (var db = new PhotoShareContext())
            {
                var album = db.Albums
                    .Where(x => x.Name == albumName)
                    .SingleOrDefault();

                if (album == null)
                {
                    throw new ArgumentException($"Album {albumName} not found!");
                }

                if (db.Pictures.Where(x => x.Album == album).Any(x => x.Title == picTitle))
                {
                    throw new ArgumentException($"Photo with such name already exists in {albumName}");
                }

                var picture = new Picture()
                {
                    Album = album,
                    Title = picTitle,
                    Path = path
                };
                db.Pictures.Add(picture);
                db.SaveChanges();

                return $"Picture {picTitle} added to {albumName}!";
            }
        }
    }
}
