using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;


namespace WpfAppCourse.ViewModels
{
    public class AdminUsersViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<User> users;
        private User selectedUser;
        private string searchText;

        public ObservableCollection<User> Users
        {
            get => users;
            set { users = value; OnPropertyChanged(); }
        }

        public User SelectedUser
        {
            get => selectedUser;
            set { selectedUser = value; OnPropertyChanged();
                (DeleteUserCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        public string SearchText
        {
            get => searchText;
            set { searchText = value; OnPropertyChanged(); }
        }

        public ICommand DeleteUserCommand { get; }
        public ICommand SearchCommand { get; }

        public AdminUsersViewModel()
        {
            Users = new ObservableCollection<User>();
            LoadUsers();

            DeleteUserCommand = new RelayCommand(() => DeleteUser(SelectedUser),
     () => SelectedUser != null);
            SearchCommand = new RelayCommand(SearchUsers);
        }

        private void LoadUsers()
        {
            using (var db = new AppContext())
            {
                Users = new ObservableCollection<User>(db.AllUsers.ToList());
            }
        }

        private void DeleteUser(User user)
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Пожалуйста, выберите запись для удаления.",
                                "Удаление",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Вы уверены, что хотите удалить \"{SelectedUser.login}\"?\nБудут удалены все связанные записи на процедуры.",
                                         "Подтверждение удаления",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                using (var db = new AppContext())
                {
                    var userToDelete = db.AllUsers.FirstOrDefault(s => s.ID == SelectedUser.ID);
                    if (userToDelete != null)
                    {
                        // Удаление отзывов пользователя
                        var relatedReviews = db.Reviews.Where(r => r.UserId == userToDelete.ID).ToList();
                        db.Reviews.RemoveRange(relatedReviews);

                        // Удаление записей на процедуры
                        var relatedReservs = db.Reserv.Where(r => r.User_Id == userToDelete.ID).ToList();
                        db.Reserv.RemoveRange(relatedReservs);

                        // Удаление пользователя
                        db.AllUsers.Remove(userToDelete);

                        db.SaveChanges();
                        MessageBox.Show("Пользователь и все связанные данные удалены.", "Удалено", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }


                LoadUsers();
            }
        }




        private void SearchUsers()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                LoadUsers();
                return;
            }

            var lowerSearch = SearchText.Trim().ToLower();

            using (var db = new AppContext())
            {
                var allUsers = db.AllUsers.ToList(); // загружаем в память
                var filtered = allUsers.Where(u =>
       (!string.IsNullOrEmpty(u.login) && u.login.ToLower().Contains(lowerSearch)) ||
       (!string.IsNullOrEmpty(u.name) && u.name.ToLower().Contains(lowerSearch)) ||
       (!string.IsNullOrEmpty(u.password) && u.password.ToLower().Contains(lowerSearch)) ||
       (!string.IsNullOrEmpty(u.surname) && u.surname.ToLower().Contains(lowerSearch)) ||
       (!string.IsNullOrEmpty(u.phone) && u.phone.ToLower().Contains(lowerSearch)) ||
       (!string.IsNullOrEmpty(u.e_mail) && u.e_mail.ToLower().Contains(lowerSearch)) ||
       (!string.IsNullOrEmpty(u.a_or_u) && u.a_or_u.ToLower().Contains(lowerSearch)) ||
       (int.TryParse(lowerSearch, out int idSearch) && u.ID == idSearch)
   ).ToList();

                Users = new ObservableCollection<User>(filtered);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
