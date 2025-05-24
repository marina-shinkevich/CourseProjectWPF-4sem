using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using static WpfAppCourse.User;

namespace WpfAppCourse
{
    

    internal class AppContext:DbContext
    {
        public DbSet<User> AllUsers { get; set; }
        public DbSet<MainServices> MainServices { get; set; }
        public DbSet<MainSpecialists> MainSpecialists { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Reserv> Reserv { get; set; }



        public AppContext() : base("WpfAppCourse.Properties.Settings.YOUTHLAB_BASEConnectionString1")
        {
            Database.SetInitializer<AppContext>(null);
        }


    }
}
