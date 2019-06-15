using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace JENCORECORP
{
    public class Applications
    {
        public Applications()
        {
            MyApplications = new ObservableCollection<ApplicationTile>();
            Employee = new ApplicationTile() { Name = "1", Color = "#FF4DAEB5", Header = "Employee", SlideImage = "/Images/1361558363_new_window.png",CanSlide = true, Icon = "/Images/1361558396_x.png", Description = "Employee Deatils" };
            Materials = new ApplicationTile() { Name = "2", Color = "#FF555BBE", Icon = "/Images/1361558396_x.png", Header = "Materials", Description = "Materials Deatils", SlideImage = "/Images/1361558396_x.png" };
            Supplier = new ApplicationTile() { Name = "3", Color = "#FFD68513", Icon = "/Images/1361558396_x.png", Description = "Supplier Deatils", Header = "Supplier" };
            Help = new ApplicationTile() { Name = "4", Color = "#FF6D9542", Icon = "/Images/1394195680_help.png", Description = "Help", Header = "Help", HoverIcon = "/Images/1394195680_help.png" };
        }

        private ObservableCollection<ApplicationTile> apps;
        public ObservableCollection<ApplicationTile> MyApplications
        {
            get { return apps; }
            set { apps = value; }
        }

        private ApplicationTile help;
        public ApplicationTile Help
        {
            get { return help; }
            set { help = value; }
        }

        private ApplicationTile employee;
        public ApplicationTile Employee
        {
            get { return employee; }
            set { employee = value; }
        }

        private ApplicationTile materials;

        public ApplicationTile Materials
        {
            get { return materials; }
            set { materials = value; }
        }

        private ApplicationTile supplier;

        public ApplicationTile Supplier
        {
            get { return supplier; }
            set { supplier = value; }
        }
    }
}
