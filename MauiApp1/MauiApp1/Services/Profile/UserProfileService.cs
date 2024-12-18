using Identity_Application.Contracts.Profile.Commands.UpdateProfile;
using Identity_Application.Contracts.Profile.Responses;
using Identity_Application.Contracts.UserScores.Commands.Create;
using Identity_Application.Contracts.UserScores.Commands.Update;
using Identity_Application.Contracts.UserScores.Response;
using System.Net.Http.Json;
using System.Net;

namespace MauiApp1.Services.Profile
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IdentityHttpClient _identityHttpClient;

        public UserProfileService(IdentityHttpClient identityHttpClient)
        {
            _identityHttpClient = identityHttpClient;
        }
        public async Task<string> Update(UpdateProfileCommand command)
        {
            try
            {
                var result = await _identityHttpClient.PutAsJsonAsync("Profile", command);

                if (result.IsSuccessStatusCode)
                    return "";

                if (result.StatusCode == HttpStatusCode.BadRequest)
                    return result.RequestMessage.ToString();

                return "Server is not responding, please try later";

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                return "Server is not responding, please try later";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "An error occurred";
            }
        }

        public async Task<UserProfileResponse> Get(string userName = null)
        {
            var result = await _identityHttpClient
              .GetFromJsonAsync<UserProfileResponse>($"Profile{(userName != null ? "?userName=" + Uri.EscapeDataString(userName) : "")}");
            return result;
        }

        public async Task<List<UserScoresResponse>> GetUserScoreByUser()
        {
            var result = await _identityHttpClient.GetFromJsonAsync<List<UserScoresResponse>>("Profile/GetEducationsByUser");
            return result;
        }

        public async Task<HttpResponseMessage> CreateUserScoreAsуnc(CreateUserScoreDetailCommand command)
        {
            var response = await _identityHttpClient.PostAsJsonAsync("Profile/CreateUserScoreDetail", command);
            return response;
        }

        public async Task<HttpResponseMessage> UpdateUserScoreAsync(UpdateUserScoreDetailCommand command)
        {
            var response = await _identityHttpClient.PutAsJsonAsync("Profile/UpdateUserScoreDetail", command);
            return response;
        }

        public async Task<HttpResponseMessage> DeleteUserScoreAsync(Guid id)
        {
            var respose = await _identityHttpClient.DeleteAsync($"Profile/DeleteUserScoreDetail/{id}");
            return respose;
        }



        public async Task<bool> SendConfirmationCode(string phoneNumber)
        {
            var response = await _identityHttpClient.GetFromJsonAsync<bool>($"sms/send_code?PhoneNumber={phoneNumber}");

            return response;
        }

        public async Task<List<UserScoresResponse>> GetAllUserScores()
        {
            var result = await _identityHttpClient.GetFromJsonAsync<List<UserScoresResponse>>("Profile/GetAllUserScore");
            return result;
        }

    }
}
