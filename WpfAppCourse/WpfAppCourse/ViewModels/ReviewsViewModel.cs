using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfAppCourse.ViewModels
{
    public class ReviewsViewModel : ViewModel
    {
        private ObservableCollection<ReviewItem> _reviewItems;
        private string _feedbackText;
        private User _currentUser;

        public ObservableCollection<ReviewItem> ReviewItems
        {
            get => _reviewItems;
            set { _reviewItems = value; OnPropertyChanged(); }
        }

        public string FeedbackText
        {
            get => _feedbackText;
            set { _feedbackText = value; OnPropertyChanged(); }
        }

        public ICommand SubmitReviewCommand { get; }
        public ICommand FeedbackTextFocusCommand { get; }
        public ICommand FeedbackTextLostFocusCommand { get; }

        public ReviewsViewModel()
        {
            _currentUser = App.CurrentUser; // Получаем текущего пользователя

            ReviewItems = new ObservableCollection<ReviewItem>();
            SubmitReviewCommand = new RelayCommand(SubmitReview);
            FeedbackTextFocusCommand = new RelayCommand(FeedbackTextGotFocus);
            FeedbackTextLostFocusCommand = new RelayCommand(FeedbackTextLostFocus);

            LoadReviews();
        }

        private void LoadReviews()
        {
            ReviewItems.Clear();

            using (var db = new AppContext())
            {
                var items = (from r in db.Reviews
                             join u in db.AllUsers on r.UserId equals u.ID
                             orderby r.CreatedAt descending
                             select new ReviewItem
                             {
                                 UserName = u.surname,
                                 Text = r.Text,
                                 CreatedAt = r.CreatedAt
                             }).ToList();

                foreach (var item in items)
                    ReviewItems.Add(item);
            }
        }

        private void SubmitReview()
        {
            var text = FeedbackText.Trim();

            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }

            using (var db = new AppContext())
            {
                int newId = db.Reviews.Any() ? db.Reviews.Max(r => r.ID) + 1 : 1;

                var review = new Reviews
                {
                    ID = newId,
                    UserId = _currentUser.ID,
                    Text = text,
                    CreatedAt = DateTime.Now
                };

                db.Reviews.Add(review);
                db.SaveChanges();

                // Добавляем новый отзыв в коллекцию
                ReviewItems.Insert(0, new ReviewItem
                {
                    UserName = _currentUser.surname,
                    Text = text,
                    CreatedAt = DateTime.Now
                });
            }

            FeedbackText = ""; // Очистить текстовое поле
        }

        private void FeedbackTextGotFocus()
        {
            if (FeedbackText == "Введите свой отзыв")
            {
                FeedbackText = "";
            }
        }

        private void FeedbackTextLostFocus()
        {
            if (string.IsNullOrWhiteSpace(FeedbackText))
            {
                FeedbackText = "Введите свой отзыв";
            }
        }
    }
}
