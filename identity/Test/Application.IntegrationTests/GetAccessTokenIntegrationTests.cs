﻿using FluentAssertions;
using Identity_Application.Contracts.User.Commands.LoginUser;
using Identity_Application.Contracts.User.Commands.RegisterUser;
using Identity_Application.Contracts.User.Queries;
using Identity_Application.Contracts.User.Responses;
using System.Net.Http.Json;

namespace Application.IntegrationTests
{
    [TestFixture]
    public class GetAccessTokenIntegrationTests : BaseTest
    {
        private JwtTokenResponse LoginResponse { get; set; }

        [SetUp]
        public async Task SetUp()
        {
            if (LoginResponse == null)
            {
                var registerCommand1 = new RegisterUserCommand
                {
                    Email = "test123@example.com",
                    Password = "password@#12P",
                    FirstName = "Alex",
                    Username = "@Alex111122",
                    LastName = "Makedonsky",
                    PhoneNumber = "+992123156789",
                    Application = "32wrgoi;l;",
                    Role = "wqu;k65"
                };
                var loginCommand1 = new LoginUserCommand { Username = "@Alex111122", Password = "password@#12P" };

                await RegisterUser(registerCommand1);
                LoginResponse = await LoginUser(loginCommand1);
            }
        }

        [Test]
        public async Task Refresh_ValidRefreshAndAccessToken_OkResult()
        {
            //Arrange
            var request = new GetAccessTokenUsingRefreshTokenQuery()
            {
                AccessToken = LoginResponse.AccessToken,
                RefreshToken = LoginResponse.RefreshToken
            };
            await AddAuthorizationAsync();
            //Act
            var response = await _client.PostAsJsonAsync("api/Auth/refresh", request);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.That(response, Is.Not.Null);
        }

        [Test]
        public async Task Refresh_InvalidRefreshToken_BadRequestResult()
        {
            //Arrange
            var request = new GetAccessTokenUsingRefreshTokenQuery
            {
                AccessToken = LoginResponse.AccessToken,
                RefreshToken = "sdf" //invalid refreshToken
            };

            //Act
            var response = await _client.PostAsJsonAsync("api/Auth/refresh", request);

            //Assert
            response.IsSuccessStatusCode.Should().Be(false);
        }

        [Test]
        public async Task Refresh_InvalidAccessToken_BadRequestResult()
        {
            //Arrange
            var request = new GetAccessTokenUsingRefreshTokenQuery
            {
                AccessToken = "sdf", //invalid accessToken
                RefreshToken = LoginResponse.RefreshToken
            };

            //Act
            var response = await _client.PostAsJsonAsync("api/Auth/refresh", request);

            //Assert
            response.IsSuccessStatusCode.Should().Be(false);
        }

        [Test]
        public async Task Refresh_NonRelatedTokens_BadRequestResult()
        {
            //Arrange
            var registerCommand2 = new RegisterUserCommand
            {
                Email = "test2@example.com",
                Password = "password@#12P2",
                FirstName = "Alex",
                Username = "@Alex222",
                LastName = "Makedonsky",
                PhoneNumber = "+992423456711",
                Application = "32wrgoi;l;",
                Role = "wqu;k65"
            };
            var loginCommand2 = new LoginUserCommand { Username = "@Alex222", Password = "password@#12P2" };

            await RegisterUser(registerCommand2);
            var loginResponse2 = await LoginUser(loginCommand2);

            var request = new GetAccessTokenUsingRefreshTokenQuery()
            {
                AccessToken = LoginResponse.AccessToken,
                RefreshToken = loginResponse2.RefreshToken
            };
            //Act
            var response = await _client.PostAsJsonAsync("api/Auth/refresh", request);

            //Assert
            response.IsSuccessStatusCode.Should().Be(false);
        }


        private async Task RegisterUser(RegisterUserCommand command)
        {
            var response = await _client.PostAsJsonAsync("api/Auth/register", command);
            response.EnsureSuccessStatusCode();
        }

        private async Task<JwtTokenResponse> LoginUser(LoginUserCommand command)
        {
            var response = await _client.PostAsJsonAsync("api/Auth/login", command);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<JwtTokenResponse>();
        }
    }
}
