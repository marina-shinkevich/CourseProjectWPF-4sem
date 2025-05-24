using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppCourse
{
    [Table("Reserv")]
    public class Reserv
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int ID { get; set; }
        private DateTime date;
        private TimeSpan time;
        private int service_Id;
        private int master_Id;
        private int user_Id;
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        public TimeSpan Time
        {
            get { return time; }
            set { time = value; }
        }
        public int Service_Id
        {
            get { return service_Id; }
            set { service_Id = value; }
        }
        public int Master_Id
        {
            get { return master_Id; }
            set { master_Id = value; }
        }
        public int User_Id
        {
            get { return user_Id; }
            set { user_Id = value; }
        }
        
        public Reserv() { }

        public Reserv(int id, DateTime Date, TimeSpan Time, int Master_Id, int Service_Id, int User_Id)
        {


            ID = id;
            date= Date;
            time = Time;
            master_Id = Master_Id;
            service_Id = Service_Id;
            user_Id = User_Id;

        }
    }
}
