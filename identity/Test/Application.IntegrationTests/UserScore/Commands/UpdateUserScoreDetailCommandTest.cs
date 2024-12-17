using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Net;
using Identity_Domain.Entities;
using Identity_Application.Contracts.UserScores.Commands.Update;

namespace Application.IntegrationTests.UserScore.Commands
{
    public class UpdateUserScoreDetailCommandTest : BaseTest
    {
        [Test]
        public async Task UpdateUserScoresDetailCommand_ShouldUpdateUserScoresDetailCommand_Success()
        {
            await AddApplicantAuthorizationAsync();

            var userScore = new UserScores()
            {
                Score = 123,
                LastUpdated = DateTime.Now,
                IncorrectQuestion = "bla bla 2*2=5 ответ 4;  2*2=4",
                UserId = Applicant.Id
            };

            await AddEntity(userScore);

            var command = new UpdateUserScoreDetailCommand()
            {
                Id = userScore.Id,
                Score = 456,
                IncorrectQuestion = userScore.IncorrectQuestion,
                LastUpdated= DateTime.Now,

            };

            var response2 = await _client.PutAsJsonAsync("/api/Profile/UpdateUserScoreDetail", command);

            response2.EnsureSuccessStatusCode();
        }
    }
}
