using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppCourse
{
    public class User
    {
        public int ID { get; set; }
        private string Login, Name, Password, Phone, E_mail, Surname, A_or_U;

        public string login
        {
            get { return Login; }
            set { Login = value; }
        }

        public string name
        {
            get { return Name; }
            set { Name = value; }
        }
        public string password
        {
            get { return Password; }
            set { Password = value; }
        }
        public string phone
        {
            get { return Phone; }
            set { Phone = value; }
        }
        public string e_mail
        {
            get { return E_mail; }
            set { E_mail = value; }
        }

        public string surname
        {
            get { return Surname; }
            set { Surname = value; }
        }

        public string a_or_u {
            get { return A_or_U; }
            set { A_or_U = value; }
            }

        public User() { }

        public User(int id, string login, string name, string password, string surname, string e_mail, string phone,string a_or_u)
        {
            ID = id;
            Login = login;
            Name = name;
            Password = password;
            Surname = surname;
            E_mail = e_mail;
            Phone = phone;
            A_or_U=a_or_u;
          
        }
      
    }

}

