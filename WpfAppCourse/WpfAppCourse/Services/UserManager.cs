using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppCourse.Services
{
    public class UserManager
    {
        public static bool TryLogin(string login, string password, out User user)
        {
            using (var db = new AppContext())
            {
                user = db.AllUsers.FirstOrDefault(u => u.login == login && u.password == password);
                if (user != null)
                {
                    App.CurrentUser = user;
                    return true;
                }
                return false;
            }
        }


        private readonly AppContext _db = new AppContext();

        public bool RegisterUser(string login, string name, string surname, string password, string email, string phone)
        {
            if (_db.AllUsers.Any(u => u.login.Equals(login, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            int id = new Random().Next();
            User user = new User(id, login, name, password, surname, email, phone, "U");

            _db.AllUsers.Add(user);
            _db.SaveChanges();
            return true;
        }
    }
}

