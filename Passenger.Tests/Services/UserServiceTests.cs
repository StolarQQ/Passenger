using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
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
            var mapperMock = new Mock<IMapper>();
            var encrypterMock = new Mock<IEncrypter>();

            // Declarative 
            var driverRepositoryMock = Mock.Of<IDriverRepository>();


            var userService = new UserService(userRepositoryMock.Object, encrypterMock.Object, mapperMock.Object);
            await userService.RegisterAsync("user@gmail.com", "username", "secret","user");

            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);

            // 17:58 Task TODO from 12 Gankewicz

        }
    }
}