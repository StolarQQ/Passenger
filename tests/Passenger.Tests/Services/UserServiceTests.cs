using System;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Services;
using Xunit;

namespace Passenger.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task registerAsync_should_invoke_add_async_once_on_repository()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            encrypterMock.Setup(x => x.GetSalt()).Returns("salt");
            encrypterMock.Setup(x => x.GetHash(It.IsAny<string>(), It.IsAny<string>())).Returns("hash");
            var mapperMock = new Mock<IMapper>();
            var userId = Guid.NewGuid();
            
            
            var userService = new UserService(userRepositoryMock.Object, encrypterMock.Object, mapperMock.Object);
            await userService.RegisterAsync(userId, "user@gmail.com", "andrew", "secret","user");

            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task when_calling_register_async_and_user_exist_in_database_method_add_async_should_never_be_invoke()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            encrypterMock.Setup(x => x.GetSalt()).Returns("salt");
            encrypterMock.Setup(x => x.GetHash(It.IsAny<string>(), It.IsAny<string>())).Returns("hash");
            var mapperMock = new Mock<IMapper>();
            var userId = Guid.NewGuid();
            var user = new User(Guid.NewGuid(), "user1@email.com", "user1", "user", "secret", "salt");

            
            var userService = new UserService(userRepositoryMock.Object, encrypterMock.Object, mapperMock.Object);
            await userService.RegisterAsync(userId, "user@gmail.com", "andrew", "secret", "user");

            userRepositoryMock.Setup(x => x.GetAsync("user@gmail.com")).ReturnsAsync(user);

            userRepositoryMock.Verify(x => x.AddAsync(user), Times.Never);
        }

        [Fact]
        public async Task when_calling_get_async_and_user_exists_it_should_invoke_user_repository_get_async()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            encrypterMock.Setup(x => x.GetSalt()).Returns("salt");
            encrypterMock.Setup(x => x.GetHash(It.IsAny<string>(), It.IsAny<string>())).Returns("hash");
            var mapperMock = new Mock<IMapper>();

            var userService = new UserService(userRepositoryMock.Object, encrypterMock.Object, mapperMock.Object);
            await userService.GetAsync("user1@gmail.com");

            userRepositoryMock.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Once);
        }
    }
}