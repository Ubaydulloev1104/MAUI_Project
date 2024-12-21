using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiApp1
{
    public partial class LoginPage : ContentPage
    {
        private readonly HttpClient _httpClient;

        public LoginPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Get username and password from UI
                string username = UsernameEntry.Text;
                string password = PasswordEntry.Text;

                // Create request body
                var requestBody = new Dictionary<string, string>
                {
                    { "username", username },
                    { "password", password }
                };

                // Serialize request body to JSON
                var jsonContent = JsonSerializer.Serialize(requestBody);
                var stringContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                // Send POST request
                var response = await _httpClient.PostAsync("https://localhost:7279/api/Auth/login", stringContent);

                // Check response status
                if (response.IsSuccessStatusCode)
                {
                    // Deserialize response
                    var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();

                    // Store tokens (e.g., in SecureStorage)
                    SecureStorage.Default.SetAsync("AccessToken", authResponse.AccessToken);
                    SecureStorage.Default.SetAsync("RefreshToken", authResponse.RefreshToken);

                    // Navigate to the next page
                    await Navigation.PushAsync(new MainPage());

                }
                else
                {
                    // Handle error (e.g., display error message)
                    await DisplayAlert("Error", "Invalid username or password.", "OK");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public class AuthResponse
        {
            public string AccessToken { get; set; }
            public string RefreshToken { get; set; }
            public DateTime AccessTokenValidateTo { get; set; }
        }
    }
}
