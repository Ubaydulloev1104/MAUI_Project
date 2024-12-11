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
        Task<List<UserScoresResponse>> GetEducationsByUser();
        Task<List<UserScoresResponse>> GetAllEducations();
        Task<HttpResponseMessage> CreateEducationAsуnc(CreateUserScoreDetailCommand command);
        Task<HttpResponseMessage> UpdateEducationAsync(UpdateUserScoreDetailCommand command);
        Task<HttpResponseMessage> DeleteEducationAsync(Guid id);

        Task<bool> SendConfirmationCode(string phoneNumber);
    }
}
