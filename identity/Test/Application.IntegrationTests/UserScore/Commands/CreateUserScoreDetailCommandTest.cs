using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Net;
using Identity_Application.Contracts.UserScores.Commands.Create;

namespace Application.IntegrationTests.UserScore.Commands
{
    public class CreateUserScoreDetailCommandTest : BaseTest
    {
        [Test]
        public async Task CreateUserScoreDetailCommand_ShouldCreateUserScoreDetailCommand_Success()
        {
            await AddApplicantAuthorizationAsync();

            var command = new CreateUserScoreDetailCommand()
            {
                Score = 123,
                LastUpdated = DateTime.Now,
                IncorrectQuestion = "bla bla 2*2=5 ответ 4;  2*2=4",
                
            };

            var response = await _client.PostAsJsonAsync("/api/Profile/CreateUserScoreDetail", command);
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task CreateUserScoreDetailCommand_ShouldCreateUserScoreDetailCommand_Duplicate()
        {
            await AddApplicantAuthorizationAsync();

            var command = new CreateUserScoreDetailCommand()
            {
                Score = 123,
                LastUpdated = DateTime.Now,
                IncorrectQuestion = "bla bla 2*2=5 ответ 4;  2*2=4",
               
            };


            var response = await _client.PostAsJsonAsync("/api/Profile/CreateUserScoreDetail", command);
            Assert.That(HttpStatusCode.OK == response.StatusCode);
 
            response = await _client.PostAsJsonAsync("/api/Profile/CreateUserScoreDetail", command);

            Assert.That(HttpStatusCode.BadRequest == response.StatusCode);
            Assert.That((await response.Content.ReadFromJsonAsync<ProblemDetails>()).Detail.Contains("Education detail already exists"), Is.Not.False);
        }
    }
}
