using Identity_Application.Contracts.User.Commands.RegisterUser;
using MauiApp1.Services.Auth;

namespace MauiApp1;

public partial class RegisterPage : ContentPage
{
    private readonly IAuthService _serverClient;
    public RegisterPage()
    {
        InitializeComponent();
    }
    private async void OnRegisterButtonClicked(object sender, EventArgs e)
    {

        LoadingIndicator.IsVisible = true;// �������� ��������� ��������\
        await Task.Delay(2000);
        Console.WriteLine("����������� ������...");

        var command = new RegisterUserCommand
        {
            Email = EmailEntry.Text?.Trim(),
            FirstName = FirstNameEntry.Text?.Trim(),
            LastName = LastNameEntry.Text?.Trim(),
            PhoneNumber = PhoneNumberEntry.Text?.Trim(),
            Username = UsernameEntry.Text?.Trim(),
            Password = PasswordEntry.Text,
            ConfirmPassword = ConfirmPasswordEntry.Text,
            Role = RoleEntry.Text
        };

        // �������� �����
        if (string.IsNullOrWhiteSpace(command.Email) ||
            string.IsNullOrWhiteSpace(command.FirstName) ||
            string.IsNullOrWhiteSpace(command.LastName) ||
            string.IsNullOrWhiteSpace(command.PhoneNumber) ||
            string.IsNullOrWhiteSpace(command.Username) ||
            string.IsNullOrWhiteSpace(command.Password) ||
            string.IsNullOrWhiteSpace(command.ConfirmPassword))
        {
            Console.WriteLine("������: �� ��� ���� ���������.");
            ErrorMessageLabel.Text = "��� ���� ������ ���� ���������!";
            ErrorMessageLabel.IsVisible = true;
            return;
        }

        if (command.Password != command.ConfirmPassword)
        {

            ErrorMessageLabel.Text = "������ �� ���������!";
            ErrorMessageLabel.IsVisible = true;
            return;
        }

        ErrorMessageLabel.IsVisible = false;
        var responseMessage = await _serverClient.RegisterUserAsync(command);
        LoadingIndicator.IsVisible = false; // ������ ��������� ��������
        if (string.IsNullOrEmpty(responseMessage))
        {

            await DisplayAlert("�������", "����������� ������ �������!", "OK");
            await Navigation.PopAsync(); // ������� �� ���������� ��������
        }
        else
        {
            ErrorMessageLabel.Text = responseMessage;
            ErrorMessageLabel.IsVisible = true;
        }
    }

}
