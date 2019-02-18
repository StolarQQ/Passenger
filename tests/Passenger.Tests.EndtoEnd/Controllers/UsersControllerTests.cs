using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Passenger.Api;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.DTO;
using Xunit;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace Passenger.Tests.EndtoEnd.Controllers
{
    public class UsersControllerTests : ControllerTestsBase
    {
        public UsersControllerTests(WebApplicationFactory<Startup> fixture) : base(fixture)
        {
          
        }
        
        [Fact]
        public async Task given_invalid_email_user_should_not_exist()
        {
            var email = "user1000@email.com";
            var response = await Client.GetAsync($"user/{email}");
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task given_unique_email_user_should_be_created()
        {
            var command = await CreateDummyUser();
            var payload = GetPayload(command);
            var response = await Client.PostAsync("user", payload);

            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Created);
            response.Headers.Location.ToString().Should().BeEquivalentTo("CreatedUser");

            var user = await GetUserAsync(command.Email);
            user.Email.Should().BeEquivalentTo(command.Email);
        }

        [Fact]
        public async Task browse_all_user_should_be_not_null()
        {
            var command = await CreateDummyUser();
            var payload = GetPayload(command);
            await Client.PostAsync("user", payload);

            var response = await Client.GetAsync("user");
            var responseString = await response.Content.ReadAsStringAsync();

            var test = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(responseString);

            test.Should().NotBeNullOrEmpty();
        }

        private async Task<UserDto> GetUserAsync(string email)
        {
            var response = await Client.GetAsync($"user/{email}");
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<UserDto>(responseString);
        }

        private async Task<CreateUser> CreateDummyUser()
        {
            var command = new CreateUser
            {
                Email = "styblera@o2.pl",
                Password = "secret",
                Username = "testtest",
                Role = "user"
            };

            return command;
        }  
    }
}