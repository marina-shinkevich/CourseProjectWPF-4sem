using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using WpfAppCourse.ViewModels;

namespace WpfAppCourse.Views
{
    /// <summary>
    /// Логика взаимодействия для Signup.xaml
    /// </summary>
    public partial class Signup : Page
    {
        private SignupViewModel _viewModel;

        public Signup()
        {
            InitializeComponent();
            _viewModel = new SignupViewModel();
            _viewModel.NavigateToLogin += () =>
            {
                ((MainWindow)Application.Current.MainWindow).LoadLogin();
            };
            this.DataContext = _viewModel;
        }



    }
}
