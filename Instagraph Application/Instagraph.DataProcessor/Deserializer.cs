using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

using Newtonsoft.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Instagraph.Data;
using Instagraph.Models;
using Instagraph.Data.Dto;
using Instagraph.DataProcessor.Dto;

namespace Instagraph.DataProcessor
{
    public class Deserializer
    {
        private static string errorMsg = "Error: Invalid data.";
        private static string successMsg = "Successfully imported {0}.";

        public static string ImportPictures(InstagraphContext context, string jsonString)
        {
            var sb = new StringBuilder();
            var pictures = JsonConvert.DeserializeObject<Picture[]>(jsonString);

            var picturesList = new List<Picture>();

            foreach (var pic in pictures)
            {
                bool isValid = !String.IsNullOrWhiteSpace(pic.Path) && pic.Size > 0;

                bool picExists = context.Pictures.Any(x => x.Path == pic.Path) &&
                    picturesList.Any(x => x.Path == pic.Path);

                if (!isValid || picExists)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                picturesList.Add(pic);
                sb.AppendLine(String.Format(successMsg, $"Picture {pic.Path}"));
            }
            context.AddRange(picturesList);
            context.SaveChanges();

            string result = sb.ToString();
            return result;
        }

        public static string ImportUsers(InstagraphContext context, string jsonString)
        {
            var sb = new StringBuilder();
            var users = JsonConvert.DeserializeObject<UserDto[]>(jsonString);

            var usersList = new List<User>();

            foreach (var userDto in users)
            {
                bool isValid = !String.IsNullOrWhiteSpace(userDto.Username) &&
                    userDto.Username.Length <= 30 &&
                    !String.IsNullOrWhiteSpace(userDto.Password) &&
                    userDto.Password.Length <= 20 &&
                    !String.IsNullOrWhiteSpace(userDto.ProfilePicture);

                var profilePic = context.Pictures.FirstOrDefault(x => x.Path == userDto.ProfilePicture);
                bool userExists = usersList.Any(x => x.Username == userDto.Username);

                if (!isValid || profilePic == null || userExists)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }
                var user = Mapper.Map<User>(userDto);

                user.ProfilePicture = profilePic;

                usersList.Add(user);
                sb.AppendLine(string.Format(successMsg, $"User {user.Username}"));
            }
            context.Users.AddRange(usersList);
            context.SaveChanges();

            string result = sb.ToString();
            return result;
        }

        public static string ImportFollowers(InstagraphContext context, string jsonString)
        {
            var sb = new StringBuilder();
            var followers = JsonConvert.DeserializeObject<UserFollowerDto[]>(jsonString);

            var followersList = new List<UserFollower>();

            foreach (var follower in followers)
            {
                int? userId = context.Users.FirstOrDefault(x => x.Username == follower.User)?.Id;
                int? followerId = context.Users.FirstOrDefault(x => x.Username == follower.Follower)?.Id;

                if (userId == null || followerId == null)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                bool alreadyFollowed = followersList.Any(x => x.UserId == userId && x.FollowerId == followerId);
                if (alreadyFollowed)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var userFollower = new UserFollower()
                {
                    UserId = userId.Value,
                    FollowerId = followerId.Value
                };
                followersList.Add(userFollower);
                sb.AppendLine(string.Format(successMsg, $"Follower {followerId} to User {userId}"));
            }
            context.UsersFollowers.AddRange(followersList);
            context.SaveChanges();

            var result = sb.ToString();
            return result;
        }

        public static string ImportPosts(InstagraphContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var xmlDoc = XDocument.Parse(xmlString);
            var posts = xmlDoc.Root.Elements();

            var postsList = new List<Post>();

            foreach (var post in posts)
            {
                string caption = post.Element("caption")?.Value;
                string username = post.Element("user")?.Value;
                string picturePath = post.Element("picture")?.Value;

                bool isValid = !String.IsNullOrWhiteSpace(caption) &&
                    !String.IsNullOrWhiteSpace(username) &&
                    !String.IsNullOrWhiteSpace(picturePath);

                if (!isValid)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var userId = context.Users.FirstOrDefault(x => x.Username == username)?.Id;
                var pictureId = context.Pictures.FirstOrDefault(x => x.Path == picturePath)?.Id;

                if(userId == null || pictureId == null)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var newPost = new Post()
                {
                    Caption = caption,
                    UserId = userId.Value,
                    PictureId = pictureId.Value
                };
                postsList.Add(newPost);
                sb.AppendLine(string.Format(successMsg, $"Post {caption}"));
            }
            context.Posts.AddRange(postsList);
            context.SaveChanges();

            var result = sb.ToString();
            return result;


        }

        public static string ImportComments(InstagraphContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var xmlDoc = XDocument.Parse(xmlString);
            var comments = xmlDoc.Root.Elements();

            var commentsList = new List<Comment>();

            foreach (var comment in comments)
            {
                string content = comment.Element("content")?.Value;
                string username = comment.Element("user")?.Value;
                string postIdString = comment.Element("post")?.Attribute("id")?.Value;

                bool isValid = !String.IsNullOrWhiteSpace(content) &&
                    !String.IsNullOrWhiteSpace(username) &&
                    !String.IsNullOrWhiteSpace(postIdString);

                if (!isValid)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                int postIdInt = int.Parse(postIdString);

                var userId = context.Users.FirstOrDefault(x => x.Username == username)?.Id;
                bool postExists = context.Posts.Any(x => x.Id == postIdInt);

                if (userId == null || !postExists)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var newComment = new Comment()
                {
                   Content = content,
                   UserId = userId.Value,
                   PostId = postIdInt
                };
                commentsList.Add(newComment);
                sb.AppendLine(string.Format(successMsg, $"Comment {content}"));
            }
            context.Comments.AddRange(commentsList);
            context.SaveChanges();

            var result = sb.ToString();
            return result;
        }
    }
}
