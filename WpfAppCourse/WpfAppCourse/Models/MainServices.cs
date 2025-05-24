using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WpfAppCourse
{
    public class MainServices
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] 
        public int ID { get; set; }
        public string Name_of_service, Description, Photo, Category;
        public int DurationMinutes;
        public int Price;
        public int durationminuts
        {
            get { return DurationMinutes; }
            set { DurationMinutes = value; }
        }
        public string category
        {
            get { return Category; }
            set { Category = value; }
        }
        public string name_of_service
        {
            get { return Name_of_service; }
            set { Name_of_service = value; }
        }
        public string description
        {
            get { return Description; }
            set { Description = value; }
        }
        public int price
        {
            get { return Price; }
            set { Price = value; }
        }
        public string photo
        {
            get { return Photo; }
            set { Photo = value; }
        }
        public MainServices() { }

        public MainServices(int id, string name_of_service, string description, int price, string photo,string category,int durationminuts) { 
        

            ID = id;
            Name_of_service= name_of_service;
            Description = description;  
            Price = price;
            Photo = photo;
            Category = category;
            DurationMinutes = durationminuts;



        }
    }
}
