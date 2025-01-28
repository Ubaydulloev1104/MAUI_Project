using Identity_Application.Contracts.User.Commands.LoginUser;
using MauiApp1.Services.Auth;
using System.Net.Http.Json;
using System.Text.Json;

namespace MauiApp1
{
    public partial class LoginPage : ContentPage
    {
        private readonly IAuthService _serverClient;

        public LoginPage()
        {
            InitializeComponent();
            
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            LoadingIndicator.IsVisible = true;// �������� ��������� ��������\
            await Task.Delay(2000);
            var username = UsernameEntry.Text;
            var password = PasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("������", "����������, ��������� ��� ����.", "��");
                return;
            }

            var token = await _serverClient.LoginUserAsync(new LoginUserCommand
            {
                Username = username,
                Password = password
            });

            if (!string.IsNullOrEmpty(token))
            {
                LoadingIndicator.IsVisible = false;
                await DisplayAlert("�����", "�� ������� ����� � �������!", "��");
                // ������� �� ������� ��������
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                LoadingIndicator.IsVisible = false;
                await DisplayAlert("������", "�������� ��� ������������ ��� ������.", "��");
            }
        }
        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {

            // ������� �� �������� �����������
            await Navigation.PushAsync(new RegisterPage());
        }
        public class AuthResponse
        {
            public string AccessToken { get; set; }
            public string RefreshToken { get; set; }
            public DateTime AccessTokenValidateTo { get; set; }
        }
    }
}
