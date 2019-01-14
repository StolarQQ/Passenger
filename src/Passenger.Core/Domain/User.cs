using System;
using System.Text.RegularExpressions;

namespace Passenger.Core.Domain
{// TODO: Check if methods could be private one,... more Regex constrains.
    public class User
    { 
        // Check this regex
        private static readonly Regex NameRegex = new Regex("^(?![_.-])(?!.*[_.-]{2})[a-zA-Z0-9._.-]+(?<![_.-])$");

        public Guid UserId { get; protected set; }
        public string Email { get; protected set; }
        public string Username { get; protected set; }
        public string FullName { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public string Role { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
       
        protected User()
        {
            
        }

        public User(Guid userid, string email, string username, string password, string salt, string role)
        {
            UserId = userid;
            SetEmail(email);
            SetUsername(username);
            SetPassword(password);
            Salt = salt;
            CreatedAt = DateTime.UtcNow;
            Role = role;
        }

        public void SetUsername(string username)
        {
            if (!NameRegex.IsMatch(username))
            {
                throw new DomainException(ErrorCodes.InvalidUsername, "Username is invalid.");
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new DomainException(ErrorCodes.InvalidUsername, "Username is invalid.");
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
            if (!Regex.IsMatch(email,
                "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$"))
            {
                throw new DomainException(ErrorCodes.InvalidEmail, $"Email: {email} is not valid");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new DomainException(ErrorCodes.InvalidEmail, "Email can not be empty.");
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
                throw new DomainException(ErrorCodes.InvalidPassword, "Password can not be empty.");
            }

            if (password.Length < 4)
            {
                throw new DomainException(ErrorCodes.InvalidPassword, "Password must contain at least 4 characters.");
            }

            if (password.Length > 100)
            {
                throw new DomainException(ErrorCodes.InvalidPassword, "Password can not contain more than 100 characters.");
            }

            if (Password == password)
            {
                return;
            }

            Password = password;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}