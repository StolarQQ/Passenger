using System;
using System.Collections.Generic;
using System.Linq;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;

namespace Passenger.Infrastructure
{
    public class InMemoryUserRepository : IUserRepository
    {
        private static ISet<User> _user = new HashSet<User>
        {
            new User("user1@gmail.com", "user1", "secret", "salt"),
            new User("user2@gmail.com", "user2", "secret", "salt"),
            new User("user3@gmail.com", "user3", "secret", "salt"),
            new User("user4@gmail.com", "user4", "secret", "salt")


        };

        public User Get(Guid id)
        {
           return _user.Single(x => x.Id == id);
        }

        public User Get(string email)
            => _user.Single(x => x.Email == email.ToLowerInvariant());

        public IEnumerable<User> GetAll()
            => _user;

        public void Add(User user)
        {
            _user.Add(user);
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            var user = Get(id);
            _user.Remove(user);
        }
    }
}