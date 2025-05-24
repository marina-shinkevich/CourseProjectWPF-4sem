using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfAppCourse.ViewModels;


namespace WpfAppCourse.Views
{
    /// <summary>
    /// Логика взаимодействия для UserProfil.xaml
    /// </summary>


    public partial class UserProfil : Page
    {
        

        public UserProfil()

        {
            InitializeComponent();
            this.DataContext = new UserProfileViewModel();


        }

   
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.LoadMainPage();
        }
    }
}
