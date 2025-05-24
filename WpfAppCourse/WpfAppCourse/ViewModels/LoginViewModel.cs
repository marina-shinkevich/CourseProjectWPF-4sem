using System.Windows;
using System.Windows.Input;
using WpfAppCourse.Services;
using WpfAppCourse.Views;

namespace WpfAppCourse.ViewModels
{
    public class LoginViewModel : ViewModel
    {
        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                if (_login != value)
                {
                    _login = value;
                    OnPropertyChanged(nameof(Login));
                }
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public ICommand EnterCommand { get; }
        public ICommand NavigateToRegisterCommand { get; }

        public LoginViewModel()
        {
            EnterCommand = new RelayCommand(LoginUser);
            NavigateToRegisterCommand = new RelayCommand(NavigateToRegister);
        }

        private void LoginUser()
        {
            if (UserManager.TryLogin(Login, Password, out var user))
            {
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                if (user.a_or_u == "A")
                    mainWindow.LoadAdminSrv();
                else
                    mainWindow.LoadMainPage();
            }
            else
            {
                MessageBox.Show("Такого пользователя нет, перейдите к регистрации");
            }
        }

        private void NavigateToRegister()
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToSignUp();
        }
    }
}
