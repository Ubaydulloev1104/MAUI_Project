using System.Text;
using System.Text.Json;

namespace MauiApp1;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }
    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        var username = usernameEntry.Text;
        var password = passwordEntry.Text;

        var loginData = new { Username = username, Password = password };
        var json = JsonSerializer.Serialize(loginData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("https://yourapiurl.com/api/auth/login", content);

        if (response.IsSuccessStatusCode)
        {
            // Успешный вход
            var token = await response.Content.ReadAsStringAsync();
            // Сохраните токен для дальнейшего использования (например, в SecureStorage)
        }
        else
        {
            // Обработка ошибки
            var errorMessage = await response.Content.ReadAsStringAsync();
            await DisplayAlert("Error", errorMessage, "OK");
        }
    }
}