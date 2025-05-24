using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfAppCourse;
using SharpVectors.Runtime;

namespace WpfAppCourse
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static YOUTHLAB_BASEDataSet Context  = new YOUTHLAB_BASEDataSet();
  
        public static User CurrentUser { get; set; }

    }


}

