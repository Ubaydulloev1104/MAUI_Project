﻿using FluentValidation;

namespace Identity_Application.Contracts.User.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
	{
		public RegisterUserCommandValidator()
		{
			RuleFor(s => s.Email).EmailAddress();
			RuleFor(s => s.Username).NotEmpty();
			RuleFor(s => s.FirstName).NotEmpty();
			RuleFor(s => s.LastName).NotEmpty();
			RuleFor(s => s.PhoneNumber).Matches(@"^(?:\d{9}|\+992\d{9}|992\d{9})$").WithMessage("Invalid phone number. Example : +992921234567, 992921234567, 921234567");
			RuleFor(s => s.Username.Length > 3);
			RuleFor(s => s.Password).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$").WithMessage("Password must have at least one lowercase letter, one uppercase letter, one digit, and one special character, with a minimum length of 8 characters.");
			RuleFor(s => !string.IsNullOrEmpty(s.Role));
			RuleFor(s => !string.IsNullOrEmpty(s.Application));
			RuleFor(s => s.ConfirmPassword).Equal(s => s.Password).WithMessage("Passwords do not match.");
		}
	}
}
