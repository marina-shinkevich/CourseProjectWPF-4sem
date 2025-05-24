using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WpfAppCourse.Services;
using WpfAppCourse.Views;

namespace WpfAppCourse.ViewModels
{
    public class SignupViewModel : INotifyPropertyChanged
    {
        private readonly UserManager _userManager = new UserManager();
        private readonly Validation _validation = new Validation();

        public FieldError Errors { get; } = new FieldError();

        private string _login;
        private string _name;
        private string _surname;
        private string _password;
        private string _email;
        private string _phone;

        public string Login { get => _login; set { _login = value; OnPropertyChanged(); Errors.LoginError = ""; } }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); Errors.NameError = ""; } }
        public string Surname { get => _surname; set { _surname = value; OnPropertyChanged(); Errors.SurnameError = ""; } }
        public string Password { get => _password; set { _password = value; OnPropertyChanged(); Errors.PasswordError = ""; } }
        public string Email { get => _email; set { _email = value; OnPropertyChanged(); Errors.EmailError = ""; } }
        public string Phone { get => _phone; set { _phone = value; OnPropertyChanged(); Errors.PhoneError = ""; } }

        public ICommand RegisterCommand { get; }
        public ICommand BackCommand { get; }

        public event Action NavigateToLogin;

        public SignupViewModel()
        {
            RegisterCommand = new RelayCommand(Register);
            BackCommand = new RelayCommand(() => NavigateToLogin?.Invoke());
        }

        private void Register()
        {
            if (string.IsNullOrWhiteSpace(Login) ||
                string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(Surname) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Phone))
            {
                MessageBox.Show("Все поля должны быть заполнены.", "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool hasErrors = _validation.ValidateUser(this, Errors);

            if (hasErrors) return;

            if (_userManager.RegisterUser(Login, Name, Surname, Password, Email, Phone))
            {
                MessageBox.Show("Регистрация прошла успешно");
                NavigateToLogin?.Invoke();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public class FieldError : INotifyPropertyChanged
    {
        private string _loginError;
        private string _nameError;
        private string _surnameError;
        private string _passwordError;
        private string _emailError;
        private string _phoneError;

        public string LoginError { get => _loginError; set { _loginError = value; OnPropertyChanged(); } }
        public string NameError { get => _nameError; set { _nameError = value; OnPropertyChanged(); } }
        public string SurnameError { get => _surnameError; set { _surnameError = value; OnPropertyChanged(); } }
        public string PasswordError { get => _passwordError; set { _passwordError = value; OnPropertyChanged(); } }
        public string EmailError { get => _emailError; set { _emailError = value; OnPropertyChanged(); } }
        public string PhoneError { get => _phoneError; set { _phoneError = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}