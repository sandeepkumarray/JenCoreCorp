using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.IO;
using RDCore;

namespace JENCORECORP
{
    public class ZoneViewModel
    {
        private ObservableCollection<Zones> _ListZones;

        public ObservableCollection<Zones> ListZones
        {
            get { return _ListZones; }
            set { _ListZones = value; }
        }

        Zones ItemZone;

        public ZoneViewModel()
        {
            ListZones = new ObservableCollection<Zones>();            
        }
    }
}
