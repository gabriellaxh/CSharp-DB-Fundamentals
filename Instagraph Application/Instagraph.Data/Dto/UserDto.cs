using System;
using System.Collections.Generic;
using System.Text;

namespace Instagraph.Data.Dto
{
    public class UserDto
    {
        private string username;
        private string password;
        private string profilePicture;

        public string Username
        {
            get
            {
                return this.username;
            }
            set
            {
                this.username = value;
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }

        public string ProfilePicture
        {
            get
            {
                return this.profilePicture;
            }
            set
            {
                this.profilePicture = value;
            }
        }
    }
}
