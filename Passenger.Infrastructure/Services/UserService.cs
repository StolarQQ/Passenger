using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Exceptions;
using ErrorCodes = Passenger.Infrastructure.Exceptions.ErrorCodes;

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

            return _mapper.Map<User, UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> BrowseAsync()
        {
            var users = await _userRepository.GetAllAsync();
            
            return _mapper.Map<IEnumerable<User>,IEnumerable<UserDto>>(users);
        }

        public async Task RegisterAsync(Guid userid, string email, string username, string password, string role)
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
            {
                throw new ServiceException(ErrorCodes.EmailInUse, $"User with email '{email}' already exists");
            }

            var salt = _encrypter.GetSalt();
            //var salt = BCrypt.Net.BCrypt.GenerateSalt();
           
            var hash = _encrypter.GetHash(password, salt);
            user = new User(userid, email, username, hash, salt, role);
            await _userRepository.AddAsync(user);
        }

        public async Task LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
            {
                throw new ServiceException(ErrorCodes.InvalidCredentials,"Invalid credentials");
            }

            var hash = _encrypter.GetHash(password, user.Salt);
            if (user.Password == hash)
            {
                return;
            }
            throw new ServiceException(ErrorCodes.InvalidCredentials, "Invalid credentials");
        }
    }
}