using Identity_Application.Contracts.Profile.Commands.UpdateProfile;
using Identity_Application.Contracts.Profile.Responses;
using Identity_Application.Contracts.UserScores.Commands.Create;
using Identity_Application.Contracts.UserScores.Commands.Update;
using Identity_Application.Contracts.UserScores.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services.Profile
{
    public interface IUserProfileService
    {
        Task<UserProfileResponse> Get(string userName = null);
        Task<string> Update(UpdateProfileCommand command);
        Task<List<UserScoresResponse>> GetUserScoreByUser();
        Task<List<UserScoresResponse>> GetAllUserScores();
        Task<HttpResponseMessage> CreateUserScoreAsуnc(CreateUserScoreDetailCommand command);
        Task<HttpResponseMessage> UpdateUserScoreAsync(UpdateUserScoreDetailCommand command);
        Task<HttpResponseMessage> DeleteUserScoreAsync(Guid id);

        Task<bool> SendConfirmationCode(string phoneNumber);
    }
}
