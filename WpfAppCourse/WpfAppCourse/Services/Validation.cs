using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
using WpfAppCourse.ViewModels;
using System.Linq;

namespace WpfAppCourse.Services
{
    public class Validation
    {
        private readonly string _namePattern = @"^[А-ЯЁA-Z][а-яёa-z]+(-[А-ЯЁA-Z][а-яёa-z]+)?$";
        private readonly AppContext _db = new AppContext();
        public bool ValidateUser(SignupViewModel viewModel, FieldError errors)
        {
            bool hasErrors = false;
            if (string.IsNullOrWhiteSpace(viewModel.Login))
            {
                errors.LoginError = "Логин не может быть пустым";
                hasErrors = true;
            }
            else if (viewModel.Login.Length > 20)
            {
                errors.LoginError = "Логин не должен превышать 20 символов";
                hasErrors = true;
            }
            else if (_db.AllUsers.Any(u => u.login.Equals(viewModel.Login, StringComparison.OrdinalIgnoreCase)))
            {
                errors.LoginError = "Такой логин уже существует";
                hasErrors = true;
            }

            if (!Regex.IsMatch(viewModel.Name, _namePattern))
            {
                errors.NameError = "Некорректно введено имя";
                hasErrors = true;
            }

            if (!Regex.IsMatch(viewModel.Surname, _namePattern))
            {
                errors.SurnameError = "Некорректно введена фамилия";
                hasErrors = true;
            }

            if (string.IsNullOrEmpty(viewModel.Password) || viewModel.Password.Length < 5)
            {
                errors.PasswordError = "Пароль должен содержать не менее 5 цифр";
                hasErrors = true;
            }
            else if (!Regex.IsMatch(viewModel.Password, @"^\d+$"))
            {
                errors.PasswordError = "Пароль должен содержать только цифры";
                hasErrors = true;
            }

            try
            {
                var emailAddress = new MailAddress(viewModel.Email);

                
                if (!Regex.IsMatch(viewModel.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                {
                    throw new FormatException("Проверьте данные");
                }
            }
            catch
            {
                errors.EmailError = "Неверный формат почты.";
                hasErrors = true;
            }


            if (!Regex.IsMatch(viewModel.Phone, @"^\+375\(\d{2}\)-\d{3}-\d{2}-\d{2}$"))
            {
                errors.PhoneError = "Телефон должен быть в формате +375(XX)-XXX-XX-XX";
                hasErrors = true;
            }

            return hasErrors;
        }
    }
}

