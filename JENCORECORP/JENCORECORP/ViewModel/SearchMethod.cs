using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.Globalization;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using iTextSharp.text.pdf;
using System.Threading;

namespace JENCORECORP
{
    public class SearchMethod
    {
        public SearchMethod()
        {
            GetAllReSource();
        }      

        public SearchMethod(string keyword)
        {
            KeyWord = keyword;
        }

        private string _keyWord;
        public string KeyWord
        {
            get { return _keyWord; }
            set { _keyWord = value; }
        }

        Dictionary<string,string> ResourcesList;

        public List<File> SearchResult;
        
        private void GetAllReSource()
        {
            ResourcesList = new Dictionary<string, string>();
            ResourceSet resourceSet = JENCORECORP.Utils.JENCORERESOURCES.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (DictionaryEntry entry in resourceSet)
            {
                ResourcesList.Add(Convert.ToString(entry.Key, CultureInfo.InvariantCulture), Convert.ToString(entry.Value, CultureInfo.InvariantCulture));
            }
        }

        public object GetSearchResult(string keyword)
        {
            object result = new object();
            KeyWord = keyword;
            foreach (var i in ResourcesList)
            {
                if (i.Key.ToLower().Contains(KeyWord) || i.Value.ToLower().Contains(KeyWord))
                {
                    result = i.Value;
                    Library.IsFromResource = true;
                }
            }

            //if (Library.IsFromResource == false)
            //{
            //    FolderFileViewModel objffvm = new FolderFileViewModel(Library.GetRootDirectory(), Library.GetRootDirectory(), null, KeyWord, false, false, false, 0);
            //    objffvm.SearchFile();
            //    result = objffvm;
            //}
            return result;
        }
    }
}
