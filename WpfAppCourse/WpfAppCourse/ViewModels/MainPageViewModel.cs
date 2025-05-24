using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using WpfAppCourse.Views;
using System.Linq;
using System.Windows.Controls;
using System;

namespace WpfAppCourse.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private ResourceDictionary dictru;
        private ResourceDictionary dicten;
        private bool isDarkTheme;

        // Команды
        public ICommand ChangeLanguageCommand { get; }
        public ICommand ThemeSwitcherCommand { get; }
        public ICommand NavigateToServicesCommand { get; }
        public ICommand NavigateToSpecialistsCommand { get; }
        public ICommand NavigateToReviewsCommand { get; }
        public ICommand NavigateToUserProfileCommand { get; }

        // Свойства
        public string ThemeIconPath => isDarkTheme
            ? "pack://application:,,,/Resources/Images/луна.png"
            : "pack://application:,,,/Resources/Images/солнце.png";

        private string selectedLanguage;
        public string SelectedLanguage
        {
            get => selectedLanguage;
            set
            {
                if (selectedLanguage != value)
                {
                    selectedLanguage = value;
                    OnPropertyChanged();
                    ChangeLanguage(selectedLanguage);
                }
            }
        }


        public MainPageViewModel()
        {
            
            ChangeLanguageCommand = new RelayCommand<string>((lang) => ChangeLanguage(lang));  // Изменение языка
            ThemeSwitcherCommand = new RelayCommand(ToggleTheme);  // Переключение темы

            // Загрузка ресурсов
            dictru = new ResourceDictionary();
            dicten = new ResourceDictionary();
            dictru.Source = new Uri("pack://application:,,,/Resources/Resources.ru.xaml", UriKind.RelativeOrAbsolute);
            dicten.Source = new Uri("pack://application:,,,/Resources/Resources.en.xaml", UriKind.RelativeOrAbsolute);

            // Установка языка и темы
            LoadSettings();
        }

        private void LoadSettings()
        {
            // Язык
            var savedLang = Properties.Settings.Default.Language;
            if (string.IsNullOrEmpty(savedLang))
            {
                savedLang = "ru"; // Язык по умолчанию
                Properties.Settings.Default.Language = savedLang;
                Properties.Settings.Default.Save();
            }

            SelectedLanguage = savedLang; // это вызовет ChangeLanguage


            // Тема
            string savedTheme = Properties.Settings.Default.Theme;
            if (string.IsNullOrEmpty(savedTheme))
            {
                savedTheme = "Dark";
                Properties.Settings.Default.Theme = savedTheme;
                Properties.Settings.Default.Save();
            }

            isDarkTheme = savedTheme == "Dark";
        }


        private void ChangeLanguage(string lang)
        {
            if (lang == "ru")
            {
                Application.Current.Resources.MergedDictionaries.Remove(dicten);
                Application.Current.Resources.MergedDictionaries.Add(dictru);
                Application.Current.Resources["Language"] = "ru";
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Remove(dictru);
                Application.Current.Resources.MergedDictionaries.Add(dicten);
                Application.Current.Resources["Language"] = "en";
            }

            Properties.Settings.Default.Language = lang;
            Properties.Settings.Default.Save();
        }

        private void ToggleTheme()
        {
            isDarkTheme = !isDarkTheme;
            string theme = isDarkTheme ? "Dark" : "Light";
            Properties.Settings.Default.Theme = theme;
            Properties.Settings.Default.Save();

            var themePath = isDarkTheme ? "DarkTheme.xaml" : "LightTheme.xaml";
            ChangeTheme(themePath);

            // Уведомляем об изменении иконки темы
            OnPropertyChanged(nameof(ThemeIconPath));
        }


        private void ChangeTheme(string themePath)
        {
            var newDict = new ResourceDictionary
            {
                Source = new Uri($"pack://application:,,,/Resources/Themes/{themePath}", UriKind.Absolute)
            };

            // Удаляем старую тему, если она подключена
            var oldDict = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("/Resources/Themes/"));

            if (oldDict != null)
                Application.Current.Resources.MergedDictionaries.Remove(oldDict);

            // Добавляем новую тему
            Application.Current.Resources.MergedDictionaries.Add(newDict);
        }

        // Реализация INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    // Реализация RelayCommand
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        public RelayCommand(Action<T> execute)
            : this(execute, null) { }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}
