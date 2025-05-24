using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WpfAppCourse
{
    public class Reviews

    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID {  get; set; }
        public string text;
        public int userid;
        public DateTime createdat;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public int UserId
        {
            get { return userid; }
            set { userid = value; }
        }
        public DateTime CreatedAt
        {
            get { return createdat; }
            set { createdat = value; }
        }
        public Reviews() { }

        public Reviews(int id, string Text, int UserId, DateTime CreatedAt)
        {
            ID = id;
            text = Text;
            userid = UserId;
            createdat = CreatedAt;
            


        }


    }
}
