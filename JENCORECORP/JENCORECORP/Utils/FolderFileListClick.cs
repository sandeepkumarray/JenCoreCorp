using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows.Controls;
using System.IO;
using System.Diagnostics;
using System.Windows;

namespace JENCORECORP
{
    class FolderFileListClick : TargetedTriggerAction<Grid>
    {
        protected override void Invoke(object parameter)
        {
            IFile file = ((ListView)((Grid)TargetObject).Children[0]).SelectedItem as IFile;
            if (file != null)
            {
                if (file.Info is FileInfo)
                {
                    FileInfo fileInfo = file.Info as FileInfo;
                    if (file.FileType == ".txt" || file.FileType == ".cs" || file.FileType == ".aspx" ||
                        file.FileType == ".xaml" || file.FileType == ".xml")
                    {
                        bool error = false;
                        ((ChildWindow)((Grid)TargetObject).Children[1]).Content = new TextViewer(new FolderFileViewModel().GetFileContent(fileInfo.FullName, out error));
                        if (error == false)
                        {
                            ((ChildWindow)((Grid)TargetObject).Children[1]).Show();
                        }
                    }
                    else if (file.FileType == ".rtf")
                    {
                        ((ChildWindow)((Grid)TargetObject).Children[1]).Content = new TextViewer(fileInfo.FullName);
                        ((ChildWindow)((Grid)TargetObject).Children[1]).Show();
                    }

                    else if (file.FileType == ".html" || file.FileType == ".htm")
                    {
                        ((ChildWindow)((Grid)TargetObject).Children[1]).Content = new UNBrowser(fileInfo.FullName);
                        ((ChildWindow)((Grid)TargetObject).Children[1]).Show();
                    }
                    else
                    {
                        Process.Start(new ProcessStartInfo(fileInfo.FullName));
                    }
                }

                if (file.Info is DirectoryInfo)
                {
                    DirectoryInfo fileInfo = file.Info as DirectoryInfo;
                    ((ListView)((Grid)TargetObject).Children[0]).ItemsSource = new FolderFileViewModel().GetChildFolderContent((FileSystemInfo)file.Info);
                    //Process.Start(new ProcessStartInfo(fileInfo.FullName));
                }
            }
        }
    }
}
