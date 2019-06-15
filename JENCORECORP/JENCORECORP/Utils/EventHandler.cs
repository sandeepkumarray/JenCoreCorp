using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JENCORECORP
{
    public static class JENCOREEventHandler
    {
        public static event EventHandler<SearchResultFromDirectory> SearchResultFromDirectory;
        public static void SearchResultFromDirectoryEND() { SearchResultFromDirectory = null; }
        public static void SearchResultFromDirectorySTART(object sender, SearchResultFromDirectory e)
        {
            if (SearchResultFromDirectory != null)
            {
                SearchResultFromDirectory(sender, e);
            }
        }
    }
}
