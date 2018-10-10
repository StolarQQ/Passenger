using System;
using System.Text.RegularExpressions;

namespace Passenger.Core.Domain
{// TODO: Check if methods could be private one, Do more Regex constrains.
    public class User
    {  
        // Check this regex
        private static readonly Regex NameRegex = new Regex("^(?![_.-])(?!.*[_.-]{2})[a-zA-Z0-9._.-]+(?<![_.-])$");

        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string Username { get; protected set; }
        public string FullName { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        protected User()
        {
            
        }

        public User(string email, string username, string password, string salt)
        {
            Id = Guid.NewGuid();
            SetEmail(email);
            SetUsername(username);
            SetPassword(password);
            Salt = salt;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetUsername(string username)
        {
            if (!NameRegex.IsMatch(username))
            {
                throw new Exception("Username is invalid.");
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new Exception("Username is invalid");
            }

            if (Username == username)
            {
                return;
            }

            Username = username.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("Email can not be empty.");
            }
            if (Email == email)
            {
                return;
            }

            Email = email.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Password can not be empty.");
            }

            if (password.Length < 4)
            {
                throw new Exception("Password must contain at least 4 characters.");
            }

            if (password.Length > 100)
            {
                throw new Exception("Password can not contain more than 100 characters.");
            }

            if (Password == password)
            {
                return;
            }

            Password = password;
            UpdatedAt = DateTime.UtcNow;

        }

        public void SetSalt(string salt)
        {
            if (string.IsNullOrWhiteSpace(salt))
            {
                throw new Exception("Salt it invalid");
            }

            if (salt.Length > 15)
            {
                throw new Exception("Salt can not contains more than 15 character");

            }

            if (Salt == salt)
            {
                return;
            }

            Salt = salt;
            UpdatedAt = DateTime.UtcNow;

        }
    }
}