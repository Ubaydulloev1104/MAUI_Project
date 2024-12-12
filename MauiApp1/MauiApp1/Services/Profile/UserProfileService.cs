using Identity_Application.Contracts.Profile.Commands.UpdateProfile;
using Identity_Application.Contracts.Profile.Responses;
using Identity_Application.Contracts.UserScores.Commands.Create;
using Identity_Application.Contracts.UserScores.Commands.Update;
using Identity_Application.Contracts.UserScores.Response;

namespace MauiApp1.Services.Profile
{
    public class UserProfileService : IUserProfileService
    {
        public Task<HttpResponseMessage> CreateEducationAsуnc(CreateUserScoreDetailCommand command)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> DeleteEducationAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserProfileResponse> Get(string userName = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserScoresResponse>> GetAllEducations()
        {
            throw new NotImplementedException();
        }

        public Task<List<UserScoresResponse>> GetEducationsByUser()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendConfirmationCode(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public Task<string> Update(UpdateProfileCommand command)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> UpdateEducationAsync(UpdateUserScoreDetailCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
