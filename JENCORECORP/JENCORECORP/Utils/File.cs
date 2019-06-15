#region Copyright Syncfusion Inc. 2001 - 2013
// Copyright Syncfusion Inc. 2001 - 2013. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace JENCORECORP
{
    public class File : IFile,INotifyPropertyChanged
    {
        private ObservableCollection<IFile> files;

        public ObservableCollection<IFile> Files
        {
            get
            {
                return files;
            }

            set
            {
                files = value;
                OnPropertyChanged("Files");
            }
        }

        private ObservableCollection<IFile> items;

        public ObservableCollection<IFile> Items
        {
            get
            {
                return items;
            }

            set
            {
                items = value;
                OnPropertyChanged("Items");
            }
        }

        public File()
        {
            Files = new ObservableCollection<IFile>();
            Items = new ObservableCollection<IFile>();
        }
        public string Name
        {
            get;

            set;
        }

        public DateTime DateModified
        {
            get;

            set;
        }

        public string FileType
        {
            get;

            set;
        }

        public double Size
        {
            get;

            set;
        }

        public object Info
        {
            get;

            set;
        }

        public ImageSource Icon
        {
            get;

            set;
        }

        private bool isSelected;

        public bool IsSelected
        {
            get 
            { 
                return isSelected; 
            }
            set 
            { 
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }


        private void OnPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }    
}
