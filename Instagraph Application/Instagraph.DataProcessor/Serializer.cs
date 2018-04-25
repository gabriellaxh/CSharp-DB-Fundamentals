using System;

using Instagraph.Data;
using System.Linq;
using Newtonsoft.Json;
using Instagraph.Data.Dto;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Instagraph.DataProcessor
{
    public class Serializer
    {
        public static string ExportUncommentedPosts(InstagraphContext context)
        {
            using (context)
            {
                var posts = context.Posts
                    .Where(x => x.Comments.Count == 0)
                    .OrderBy(z => z.Id)
                    .Select(x => new
                    {
                        Id = x.Id,
                        Picture = x.Picture.Path,
                        User = x.User.Username
                    })
                    .ToArray();

                var jsonExport = JsonConvert.SerializeObject(posts, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        DefaultValueHandling = DefaultValueHandling.Ignore
                    });

                return jsonExport;
            }
        }

        public static string ExportPopularUsers(InstagraphContext context)
        {
            using (context)
            {
                var users = context.Users
                    .Where(x => x.Posts
                                    .Any(z => z.Comments
                                              .Any(s => x.Followers.Any(b => b.FollowerId == z.UserId))))
                                              .OrderBy(x => x.Id)
                                              .Select(x => new
                                              {
                                                  User = x.Username,
                                                  Followers = x.Followers.Count
                                              }).ToArray();

                var jsonExport = JsonConvert.SerializeObject(users, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        DefaultValueHandling = DefaultValueHandling.Ignore
                    });

                return jsonExport;
            }
        }

        public static string ExportCommentsOnPosts(InstagraphContext context)
        {
            using (context)
            {
                var users = context.Users
                    .Select(x => new
                    {
                        Username = x.Username,
                        PostCommentsCount = x.Posts.Select(p => p.Comments.Count).ToArray()
                    });

                var userList = new List<UserMostCommentsDto>();

                var xmlDoc = new XDocument(new XElement("users"));

                foreach (var user in users)
                {
                    int mostComments = 0;
                    if (user.PostCommentsCount.Any())
                    {
                        mostComments = user.PostCommentsCount.OrderByDescending(x => x).First();
                    }

                    var newUser = new UserMostCommentsDto()
                    {
                        Username = user.Username,
                        MostComments = mostComments
                    };

                    userList.Add(newUser);
                }

                userList = userList.OrderByDescending(x => x.MostComments)
                    .ThenBy(x => x.Username)
                    .ToList();

                foreach (var user in userList)
                {
                    xmlDoc.Root.Add(new XElement("user",
                        new XElement("Username", user.Username),
                        new XElement("MostComments", user.MostComments)));
                }
                var result = xmlDoc.ToString();
                return result;
            }
        }
    }
}
