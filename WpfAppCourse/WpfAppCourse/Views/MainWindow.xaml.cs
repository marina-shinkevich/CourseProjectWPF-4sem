using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfAppCourse.Views
{
    public partial class MainWindow : Window
    {
        static Cursor my = new Cursor(Application.GetResourceStream(
       new Uri("pack://application:,,,/Resources/mymouse (1).cur")).Stream);
        public MainWindow()
        {
            this.Cursor = my;

            InitializeComponent();
            //LoadUserProfil();
            //LoadAdminSrv();
            LoadLogin();
            //LoadSpecialists();
            //LoadMainPage();
            //NavigateToServices();
            //LoadAdminSpecialists();
            //LoadReviews();
            //LoadAdminReviews();
            //LoadAdminUsers();
            //LoadAdminReservs();
        }
        public void LoadAdminUsers()
        {
            MainFrame.Navigate(new AdminUsers());

        }
        public void LoadAdminReviews()
        {
            MainFrame.Navigate(new AdminReviews());

        }
        public void LoadAdminReservs()
        {
            MainFrame.Navigate(new AdminReservs());

        }
        public void LoadAdminSrv()
        {
            MainFrame.Navigate(new AdminServices());

        }
        public void NavigateToPage(Page page)
        {
            MainFrame.Navigate(page);
        }
        public void LoadSpecialists()
        {
            MainFrame.Navigate(new Specialists());

        }
        public void LoadAdminSpecialists()
        {
            MainFrame.Navigate(new AdminSpecialists());

        }
        public void LoadLogin()
        {
            MainFrame.Navigate(new Login());
        }
        public void LoadUserProfil()
        {
            MainFrame.Navigate(new UserProfil());
        }
        public void LoadMainPage()
        {
            MainFrame.Navigate(new MainPage());
        }

        public void LoadReviews()
        {
            MainFrame.Navigate(new MainReviews());
        }

        public void NavigateToServices()
        {
            MainFrame.Navigate(new Services());
        }
        public void NavigateToSignUp()
        {
            MainFrame.Navigate(new Signup());
        }
        // Метод для навигации назад
        public void NavigateBack()
        {
            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
            }
        }
    }
}