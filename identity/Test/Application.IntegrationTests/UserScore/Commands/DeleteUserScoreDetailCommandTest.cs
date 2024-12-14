
using Identity_Domain.Entities;

namespace Application.IntegrationTests.UserScore.Commands
{
    public class DeleteUserScoreDetailCommandTest : BaseTest
    {
        [Test]
        public async Task DeleteUserScoreDetailCommand_ShouldDeleteUserScoreDetail_Success()
        {
            await AddApplicantAuthorizationAsync();

            var userScore = new UserScores()
            {
                Score = 123,
                LastUpdated = DateTime.Now,
                IncorrectQuestion="bla bla 2*2=5 ответ 4;  2*2=4",
                UserId = Applicant.Id
            };
            await AddEntity(userScore);

            var response = await _client.DeleteAsync($"/api/Profile/DeleteUserScoreDetail/{userScore.Id}");

            response.EnsureSuccessStatusCode();
        }
    }
}
