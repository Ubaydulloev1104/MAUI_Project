using Identity_Application.Contracts.Profile.Commands.UpdateProfile;
using System.Net.Http.Json;

namespace Application.IntegrationTests.UserProfile
{
    public class UpdateUserProfileTest : BaseTest
    {
        [Test]
        public async Task UpdateUserProfile_ShouldUpdateUserProfile_Success()
        {
            await AddApplicantAuthorizationAsync();
            var profile = new UpdateProfileCommand()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Email = Applicant.Email,
                DateOfBirth = DateTime.Now,
                PhoneNumber = "+992125456789",
                AboutMyself = "ekmden ehfisuhfshfsfehf fjshefsjhf slufh self"

            };
            var response = await _client.PutAsJsonAsync("/api/Profile", profile);

            response.EnsureSuccessStatusCode();
        }
    }
}
