using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppCourse
{
    public class MainSpecialists
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        private string name, surname, description, photo;
        private int service;
        public string FullName => $"{Name} {Surname}";
        private int experience;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string Photo
        {
            get { return photo; }
            set { photo = value; }

        }
        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }
        public int Service
        {
            get { return service; }
            set { service = value; }
        }
        public int Experience
        {
            get { return experience; }
            set { experience = value; }
        }
        public MainSpecialists(){}

        public MainSpecialists(int id, string Name, string Surname, string Description, string Photo, int Service, int Experience)
        {
            ID = id;
            name = Name;
            surname = Surname;
            description = Description;
            service = Service;
            experience = Experience;
            photo =Photo;
           
           
        }
    }
}
