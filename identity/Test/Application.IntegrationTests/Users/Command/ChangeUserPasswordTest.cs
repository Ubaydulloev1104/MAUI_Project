﻿using Identity_Application.Contracts.User.Commands.ChangePassword;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Net;

namespace Application.IntegrationTests.Users.Command
{
    public class ChangeUserPasswordTest : BaseTest
    {
        [Test]
        public async Task ChangePassword_ReturnsOk()
        {
            await AddApplicantAuthorizationAsync();

            // Arrange
            var command = new ChangePasswordUserCommand
            {
                OldPassword = "password@#12P",
                NewPassword = "password@#12P123",
                ConfirmPassword = "password@#12P123"
            };

            // Act
            var response = await _client.PutAsJsonAsync("/api/Auth/ChangePassword", command);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.That(HttpStatusCode.OK == response.StatusCode);
        }

        [Test]
        public async Task ChangePassword_IncorrectPassword()
        {
            await AddApplicantAuthorizationAsync();

            // Arrange
            var command = new ChangePasswordUserCommand
            {
                OldPassword = "password@#12P11111111",
                NewPassword = "password@#12P123",
                ConfirmPassword = "password@#12P123"
            };

            // Act
            var response = await _client.PutAsJsonAsync("/api/Auth/ChangePassword", command);

            // Assert
            Assert.That(HttpStatusCode.BadRequest == response.StatusCode);

            var responseString = (await response.Content.ReadFromJsonAsync<ProblemDetails>()).Detail;
            Assert.That("Incorrect old password" == responseString);
        }
    }
}
