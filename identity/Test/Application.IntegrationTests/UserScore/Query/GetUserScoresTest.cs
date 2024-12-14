using System.Net;

namespace Application.IntegrationTests.UserScore.Query
{
    public class GetUserScoresTest:BaseTest
    {
        [Test]
        public async Task GetUserScores_ShouldReturnUserScores_Success()
        {
            await AddApplicantAuthorizationAsync(); 
            var response = await _client.GetAsync($"/api/Profile/GetUserScoreByUser");
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task GetUserScores_ShouldReturnUserScores_AccessIsDenied()
        {
            await AddApplicantAuthorizationAsync();
            var response = await _client.GetAsync($"/api/Profile/GetUserScoreByUser?userName=amir");

            Assert.That(HttpStatusCode.Forbidden == response.StatusCode);
        }

        [Test]
        public async Task GetUserScoresByUserName_ShouldReturnUserScoresByUserName_Success()
        {
            await AddReviewerAuthorizationAsync();
            var response = await _client.GetAsync($"/api/Profile/GetUserScoreByUser?userName=@Azamjon123");

            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task GetUserScoresByUserName_ShouldReturnUserScoresByUserName_NotFound()
        {
            await AddReviewerAuthorizationAsync();
            var response = await _client.GetAsync($"/api/Profile/GetUserScoreByUser?userName=@Alex34");

            Assert.That(HttpStatusCode.NotFound == response.StatusCode);
        }
    }
}
