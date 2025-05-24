// UserProfileViewModel.cs
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

using WpfAppCourse.Views;

namespace WpfAppCourse.ViewModels
{
    public class UserProfileViewModel : ViewModel
    {
        private readonly Stack<User> _undoStack = new Stack<User>();
        private readonly Stack<User> _redoStack = new Stack<User>();
        private User _currentUser;
        private bool isDarkTheme;

        public ObservableCollection<AppointmentViewModel> Appointments { get; }
      = new ObservableCollection<AppointmentViewModel>();

        private AppointmentViewModel _selectedAppointment;

        public AppointmentViewModel SelectedAppointment
        {
            get => _selectedAppointment;
            set
            {
                _selectedAppointment = value;
                OnPropertyChanged();
            }
        }

        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged();
            }
        }

        // Свойства
        public string ThemeIconPath => isDarkTheme
            ? "pack://application:,,,/Resources/Images/луна.png"
            : "pack://application:,,,/Resources/Images/солнце.png";



        public ICommand SaveCommand { get; }
        private RelayCommand _undoCommand;
        private RelayCommand _redoCommand;

        public ICommand UndoCommand => _undoCommand;
        public ICommand RedoCommand => _redoCommand;

        public ICommand CancelAppointmentCommand { get; }
        public ICommand ThemeSwitcherCommand { get; }
        public ICommand NavigateToMainCommand { get; }

        public UserProfileViewModel()
        {
            CurrentUser = App.CurrentUser ?? new User();
            SaveCommand = new RelayCommand(Save);
            _undoCommand = new RelayCommand(Undo, () => _undoStack.Count > 0);
            _redoCommand = new RelayCommand(Redo, () => _redoStack.Count > 0);

            CancelAppointmentCommand = new RelayCommand<AppointmentViewModel>(CancelAppointment);
            ThemeSwitcherCommand = new RelayCommand(ToggleTheme);
            NavigateToMainCommand = new RelayCommand(NavigateToMain);
            LoadUserAppointments();
            LoadSettings();
            // Добавим исходное состояние для Undo
            _undoStack.Push(CloneUser(CurrentUser));
            _undoCommand.RaiseCanExecuteChanged();
        }

        private void LoadSettings()
        {
            


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



        private void Save()
        {
            // Проверка имени
            if (!IsValidName(CurrentUser.name))
            {
                MessageBox.Show("Неверное имя. Убедитесь, что оно начинается с заглавной буквы, не содержит цифр, точек и соответствует формату двойного имени.");
                return;
            }

            // Проверка фамилии
            if (!IsValidName(CurrentUser.surname))
            {
                MessageBox.Show("Неверная фамилия. Убедитесь, что она начинается с заглавной буквы, не содержит цифр, точек и соответствует формату двойной фамилии.");
                return;
            }

            // Проверка пароля
            if (string.IsNullOrEmpty(CurrentUser.password))
            {
                MessageBox.Show("Пароль не может быть пустым.");
                return;
            }
            else if (CurrentUser.password.Length < 5)
            {
                MessageBox.Show("Пароль должен содержать минимум 5 символов.");
                return;
            }
            else if (!Regex.IsMatch(CurrentUser.password, @"^\d+$"))
            {
                MessageBox.Show("Пароль должен содержать только цифры.");
                return;
            }
            // Проверка логина
            if (string.IsNullOrWhiteSpace(CurrentUser.login))
            {
                MessageBox.Show("Логин не может быть пустым.");
                return;
            }
            else if (CurrentUser.login.Length > 20)
            {
                MessageBox.Show("Логин не должен превышать 20 символов.");
                return;
            }
            using (var db = new AppContext())
            {
                // Проверка уникальности логина (исключая текущего пользователя)
                bool loginExists = db.AllUsers
                    .Any(u => u.login.Equals(CurrentUser.login, StringComparison.OrdinalIgnoreCase)
                           && u.ID != CurrentUser.ID);

                if (loginExists)
                {
                    MessageBox.Show("Такой логин уже существует. Выберите другой.");
                    return;
                }
            }
                // Проверка e-mail (только латиница)
                if (!Regex.IsMatch(CurrentUser.e_mail ?? "", @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                MessageBox.Show("Неверный формат электронной почты. Разрешены только латинские буквы.");
                return;
            }

            // Проверка телефона
            if (!Regex.IsMatch(CurrentUser.phone ?? "", @"^\+375\((25|29|33|44)\)-\d{3}-\d{2}-\d{2}$"))
            {
                MessageBox.Show("Неверный формат телефона. Используйте формат +375(xx)-xxx-xx-xx");
                return;
            }



            // Сохраняем предыдущую версию
            _undoStack.Push(CloneUser(CurrentUser));
            _redoStack.Clear();
            _undoCommand.RaiseCanExecuteChanged();
            _redoCommand.RaiseCanExecuteChanged();

            using (var db = new AppContext())
            {
                var userInDb = db.AllUsers.FirstOrDefault(u => u.ID == CurrentUser.ID);
                if (userInDb == null)
                {
                    MessageBox.Show("Пользователь не найден в базе.");
                    return;
                }

                userInDb.name = CurrentUser.name;
                userInDb.surname = CurrentUser.surname;
                userInDb.e_mail = CurrentUser.e_mail;
                userInDb.login = CurrentUser.login;
                userInDb.password = CurrentUser.password;
                userInDb.phone = CurrentUser.phone;


                db.SaveChanges();
                CurrentUser = userInDb;
                App.CurrentUser = userInDb;
                MessageBox.Show("Данные успешно сохранены!");
            }
        }

        // Метод валидации имени/фамилии
        private bool IsValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 2)
                return false;

            // Проверка двойного имени
            if (name.Contains("-"))
            {
                var parts = name.Split('-');
                if (parts.Length != 2)
                    return false;

                return parts.All(p => Regex.IsMatch(p, @"^[A-ZА-Я][a-zа-я]+$"));
            }

            return Regex.IsMatch(name, @"^[A-ZА-Я][a-zа-я]+$");
        }

        private void Undo()
        {
            _redoStack.Push(CloneUser(CurrentUser));
            CurrentUser = _undoStack.Pop();
            App.CurrentUser = CurrentUser;
            _undoCommand.RaiseCanExecuteChanged();
            _redoCommand.RaiseCanExecuteChanged();
        }

        private void Redo()
        {
            _undoStack.Push(CloneUser(CurrentUser));
            CurrentUser = _redoStack.Pop();
            App.CurrentUser = CurrentUser;
            _undoCommand.RaiseCanExecuteChanged();
            _redoCommand.RaiseCanExecuteChanged();
        }

        private void LoadUserAppointments()
        {
            var user = App.CurrentUser;
            var userId = user.ID;

            using (var context = new AppContext())
            {
                var userReserves = context.Reserv
                    .Where(r => r.User_Id == userId)
                    .OrderByDescending(r => r.Date)
                    .ThenByDescending(r => r.Time)
                    .ToList();

                var serviceIds = userReserves.Select(r => r.Service_Id).Distinct().ToList();
                var services = context.MainServices
                    .Where(s => serviceIds.Contains(s.ID))
                    .ToList();

                // Создаем объекты AppointmentViewModel
                foreach (var r in userReserves)
                {
                    var service = services.FirstOrDefault(s => s.ID == r.Service_Id);
                    if (service == null)
                        continue;

                    var appointmentViewModel = new AppointmentViewModel(service)
                    {
                        SelectedDate = r.Date,
                        TimeInput = r.Time.ToString(@"hh\:mm"),
                        ServiceName = service.name_of_service,
                        ReservationId = r.ID 
                    };


                    // Добавляем в коллекцию
                    Appointments.Add(appointmentViewModel);
                }
            }
        }


        private void CancelAppointment(AppointmentViewModel appointmentToCancel)
        {
            // Проверка входящего параметра
            if (appointmentToCancel == null)
            {
                MessageBox.Show("Пожалуйста, выберите запись для отмены",
                              "Не выбрана запись",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
                return;
            }

            // Подтверждение
            var confirmResult = MessageBox.Show(
                $"Вы точно хотите отменить запись?\n\n" +
                $"Услуга: {appointmentToCancel.ServiceName}\n" +
                $"Мастер: {appointmentToCancel.SelectedMaster?.Name ?? "не указан"}\n" +
                $"Дата: {appointmentToCancel.SelectedDate:dd.MM.yyyy}\n" +
                $"Время: {appointmentToCancel.TimeInput}",
                "Подтверждение отмены",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (confirmResult != MessageBoxResult.Yes) return;

            try
            {
                using (var context = new AppContext())
                {
                    
                    var reservation = context.Reserv
                        .FirstOrDefault(r => r.ID == appointmentToCancel.ReservationId);

                    if (reservation == null)
                    {
                        MessageBox.Show("Запись не найдена в базе данных",
                                      "Ошибка",
                                      MessageBoxButton.OK,
                                      MessageBoxImage.Error);
                        return;
                    }

                    context.Reserv.Remove(reservation);
                    int saved = context.SaveChanges();

                    if (saved > 0)
                    {
                        
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Appointments.Remove(appointmentToCancel);
                        });

                        MessageBox.Show("Запись успешно отменена",
                                      "Успех",
                                      MessageBoxButton.OK,
                                      MessageBoxImage.Information);
                    }
                }
            }
            catch (DbUpdateException dbEx)
            {
                MessageBox.Show($"Ошибка базы данных: {dbEx.InnerException?.Message}",
                              "Ошибка",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неожиданная ошибка: {ex.Message}",
                              "Ошибка",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
        }
       

        private void NavigateToMain()
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.LoadMainPage();
        }

        private static User CloneUser(User user) => new User
        {
            ID = user.ID,
            name = user.name,
            surname = user.surname,
            e_mail = user.e_mail,
            login = user.login,
            password = user.password,
            phone = user.phone
        };
    }

   
}