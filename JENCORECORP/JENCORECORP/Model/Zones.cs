using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace JENCORECORP
{
    public class Zones
    {
        public long ZoneId { get; set; }

        public string ZoneName { get; set; }

        public Color LabelColour { get; set; }

        public Color LabelColour2 { get; set; }

        public string Description { get; set; }

        public double StrokeThickness { get; set; }

        public string ControlHeader { get; set; }

        public string ProfitPercentage { get; set; }

        public string Overal { get; set; }

        public double Height { get; set; }

        public double Width { get; set; }

        public string HoverIcon { get; set; }
    }
}
