using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAppCourse.ViewModels;

namespace WpfAppCourse.Views
{
    /// <summary>
    /// Логика взаимодействия для ServiceDetails.xaml
    /// </summary>
    public partial class ServiceDetails : Page
    {
       
        public ServiceDetails(MainServices selectedService)
        {
            InitializeComponent();
            DataContext = new ServiceDetailsViewModel(selectedService);
           


        }




        private void LanguageSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LanguageSelector.SelectedItem is ComboBoxItem selectedItem && selectedItem.Tag != null)
            {
                string selectedLanguage = selectedItem.Tag.ToString();
                (DataContext as MainPageViewModel)?.ChangeLanguageCommand.Execute(selectedLanguage);
            }
        }

        private void HomeTextBlock_Click(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.LoadMainPage();
        }

        private void ServicesTextBlock_Click(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.NavigateToServices();
        }

        private void SpecialTextBlock_Click(object sender, MouseButtonEventArgs e)
        {

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.LoadSpecialists();
        }

        private void reviewsTextBox_Click(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.LoadReviews();
        }

        private void ProfilImg_Click(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.LoadUserProfil();
        }
    }
}
