using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace JENCORECORP
{
    public static class JENCOREEvents
    {
        
    }

    public class SearchResultFromDirectory : EventArgs
    {
        public SearchResultFromDirectory(RoutedEvent revent)
        {
            this.RoutedEvent = revent;
        }
        public RoutedEvent RoutedEvent { get; set; }

    }

}
