using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;
        private readonly IMapper _mapper;

        // Source of data, it could be database / list etc. 
        public UserService(IUserRepository userRepository, IEncrypter encrypter, IMapper mapper)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
            _mapper = mapper;
        }
        // TODO Learn more about Auto Mapper lib
        public async Task<UserDto> GetAsync(string email)
        {
            var user = await _userRepository.GetAsync(email);
            // Add more null checker
            // Error handling API CORE 2.0
;
            return _mapper.Map<User, UserDto>(user);
        }

        public async Task RegisterAsync(string email,string username, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
            {
                throw new Exception($"User with email '{email}' already exists");
            }

            var salt = _encrypter.GetSalt();
            var hash = _encrypter.GetHash(password, salt);
            user = new User(email, username, hash, salt);
            await _userRepository.AddAsync(user);
        }

        public async Task LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
            {
                throw new Exception("Invalid credentials");
            }

            var salt = _encrypter.GetSalt();
            var hash = _encrypter.GetHash(password, salt);
            if (user.Password == hash)
            {
                return;
            }
            throw new Exception("Invalid credentials");
        }
    }
}