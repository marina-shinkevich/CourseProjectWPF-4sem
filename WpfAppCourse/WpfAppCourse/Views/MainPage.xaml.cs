using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using WpfAppCourse.ViewModels;

namespace WpfAppCourse.Views
{
    public partial class MainPage : Page
    {
     

        public MainPage()
        {
            InitializeComponent();
            DataContext = new MainPageViewModel();


        }
        private void LanguageSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LanguageSelector.SelectedItem is ComboBoxItem selectedItem && selectedItem.Tag != null)
            {
                string selectedLanguage = selectedItem.Tag.ToString();
                (DataContext as MainPageViewModel)?.ChangeLanguageCommand.Execute(selectedLanguage);
            }
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