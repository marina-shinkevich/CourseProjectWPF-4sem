using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfAppCourse.ViewModels
{
    public class AdminReviewViewModel:ViewModel
    {
        private ObservableCollection<Reviews> _reviews;
        public ObservableCollection<Reviews> Reviews
        {
            get => _reviews;
            set { _reviews = value; OnPropertyChanged(); }
        }
        private Reviews _selectedReview;
        public Reviews SelectedReview
        {
            get => _selectedReview;
            set
            {
                _selectedReview = value;
                OnPropertyChanged();

                // Обновляем доступность команды
                (DeleteReviewCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }
        public string Text { get; set; }
        public string UserId { get; set; }
        public string CreatedAt { get; set; }


        public ICommand DeleteReviewCommand { get; }

        public AdminReviewViewModel()
        {
            LoadReviews();

            DeleteReviewCommand = new RelayCommand(
     () => DeleteReview(SelectedReview),
     () => SelectedReview != null
 );

          

        }
        private void LoadReviews()
        {
            using (var db = new AppContext())
                Reviews = new ObservableCollection<Reviews>(db.Reviews.ToList());
        }
        private void DeleteReview(Reviews review)
        {
            try

            {
                if (review == null)
                {
                    MessageBox.Show("Пожалуйста, выберите отзыв для удаления.",
                                  "Удаление",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Warning);
                    return;
                }

                var result = MessageBox.Show($"Вы уверены, что хотите удалить \"{review.ID}\"?",
                                           "Подтверждение удаления",
                                           MessageBoxButton.YesNo,
                                           MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    using (var db = new AppContext())
                    {
                        var reviewToDelete = db.Reviews.Find(review.ID);
                        if (reviewToDelete != null)
                        {
                            db.Reviews.Remove(reviewToDelete);
                            db.SaveChanges();
                            MessageBox.Show("Отзыв удален.",
                                          "Удалено",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Information);

                            // Обновляем список услуг
                            LoadReviews(); 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при удалении: {ex.Message}",
                               "Ошибка",
                               MessageBoxButton.OK,
                               MessageBoxImage.Error);
            }
        }
    }
}
