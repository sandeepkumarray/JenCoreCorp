using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Media;
using System.Drawing;
using System.Text.RegularExpressions;
using iTextSharp.text.pdf;
using System.ComponentModel;

namespace JENCORECORP
{
    public class FolderFileViewModel
    {
        public FolderFileViewModel()
        {
            SearchResult = new List<File>();
        }

        public FolderFileViewModel(string RootDir, string Dir, String[] FileExtensions, String SearchForText, bool CaseSensitive, bool UseRegularExpression, bool UseIFilter, int countVisitDepth)
        {
            SearchResult = new List<File>();
            this.Dir = Dir;
            this.SearchForText = SearchForText;
            this.CaseSensitive = CaseSensitive;
            this.UseRegularExpression = UseRegularExpression;
            this.UseIFilter = UseIFilter;
            this.FileExtensions = FileExtensions;
        }

        public List<File> SearchResult;

        /// <summary>
        /// Used to get ChildFolder Content of Particular folder
        /// </summary>
        /// <param name="fileNodeItem"></param>
        /// <returns></returns>
        public List<File> GetChildFolderContent(FileSystemInfo fileNodeItem)
        {
            List<File> children = new List<File>();
            File file = new File();
            string folder = ((fileNodeItem)).FullName;
            DirectoryInfo info = new DirectoryInfo(folder);
            if (info.Exists)
            {
                try
                {
                    DirectoryInfo[] infos = info.GetDirectories();
                    foreach (DirectoryInfo directory in infos)
                    {
                        File _file = new File();
                        _file.Name = directory.Name;
                        _file.Info = directory;

                        _file.FileType = "File Folder";
                        BitmapImage image = new BitmapImage(new Uri("/JENCORECORP;component/Images/folder.png", UriKind.RelativeOrAbsolute));                        
                       // image.Freeze();
                        _file.Icon = image;
                        _file.Icon.Freeze();
                        _file.DateModified = directory.LastWriteTime;
                        ((File)file).Files.Add(_file);
                        ((File)file).Items.Add(_file);
                        children.Add(_file);
                    }

                    FileInfo[] files = info.GetFiles();
                    foreach (FileInfo directory in files)
                    {
                        File _file = new File();
                        _file.Name = directory.Name;
                        _file.Info = directory;
                        _file.FileType = directory.Extension;
                        _file.Size = directory.Length / 1024;
                        _file.Icon = ExtractIcon(directory);
                        _file.DateModified = directory.LastWriteTime;
                        ((File)file).Items.Add(_file);
                        children.Add(_file);
                    }


                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            return children;
        }

        /// <summary>
        /// Extracts File Icons
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        private ImageSource ExtractIcon(FileInfo directory)
        {
            System.Drawing.Icon ico = System.Drawing.Icon.ExtractAssociatedIcon(directory.FullName);
            Bitmap bmp = ico.ToBitmap();
            MemoryStream strm = new MemoryStream();
            bmp.Save(strm, System.Drawing.Imaging.ImageFormat.Png);
            BitmapImage bmpImage = new BitmapImage();

            bmpImage.BeginInit();
            strm.Seek(0, SeekOrigin.Begin);
            bmpImage.StreamSource = strm;
            bmpImage.EndInit();
            bmpImage.Freeze();
            ImageSource source = bmpImage;
            return source;
        }

        /// <summary>
        /// Start searching
        /// </summary>
        public void SearchFile()
        {
            
        }

        public void GetFiles(string RootDir, string Dir, String[] FileExtensions, String SearchForText, bool CaseSensitive, bool UseRegularExpression, bool UseIFilter, int countVisitDepth)
        {
            try
            {
                if (FileExtensions != null && FileExtensions.Length > 0)
                {
                    foreach (string FileExtension in FileExtensions)
                    {
                        foreach (string File in Directory.GetFiles(Dir, FileExtension))
                        {
                            // check if filter by date/time
                            //if (filterByDate)
                            //{
                            //    DateTime fdt = new DateTime();
                            //    FileInfo finfo = new FileInfo(File);
                            //    switch (fileTimeType)
                            //    {
                            //        case FileTimeType.CreationTime:
                            //            fdt = finfo.CreationTime;
                            //            break;
                            //        case FileTimeType.LastAccessTime:
                            //            fdt = finfo.LastAccessTime;
                            //            break;
                            //        case FileTimeType.LastWriteTime:
                            //            fdt = finfo.LastWriteTime;
                            //            break;
                            //    }
                            //}


                            if (FileContainsText(File, SearchForText, CaseSensitive, UseRegularExpression, UseIFilter))
                            {
                                //mainForm.Invoke(ALBIDelegate, new Object[] { File });
                                //foreach (FileInfo directory in File)
                                //{
                                //    File _file = new File();
                                //    _file.Name = directory.Name;
                                //    _file.Info = directory;
                                //    _file.FileType = directory.Extension;
                                //    _file.Size = directory.Length / 1024;
                                //    _file.Icon = ExtractIcon(directory);
                                //    _file.DateModified = directory.LastWriteTime;
                                //    ((File)file).Items.Add(_file);
                                //    children.Add(_file);
                                //}
                            }
                        }
                    }
                }
                else
                {
                    if (Dir.ToLower().Contains(SearchForText))
                    {
                        DirectoryInfo file = new DirectoryInfo(Dir);
                        File _file = new File();
                        _file.Name = file.Name;
                        _file.Info = file;

                        _file.FileType = "File Folder";
                        BitmapImage image = new BitmapImage(new Uri("/JENCORECORP;component/Images/folder.png", UriKind.RelativeOrAbsolute));
                        _file.Icon = image;
                        _file.Icon.Freeze();
                        _file.DateModified = file.LastWriteTime;
                        SearchResult.Add(_file);
                    }
                    foreach (string File in Directory.GetFiles(Dir))
                    {
                        if (Path.GetExtension(File) == ".txt" || Path.GetExtension(File) == ".rtf" || Path.GetExtension(File) == ".doc")
                        {
                            if (FileContainsText(File, SearchForText, CaseSensitive, UseRegularExpression, UseIFilter))
                            {
                                FileInfo file = new FileInfo(File);
                                File _file = new File();
                                _file.Name = file.Name;
                                _file.Info = file;
                                _file.FileType = file.Extension;
                                _file.Size = file.Length / 1024;
                                _file.Icon = ExtractIcon(file);
                                _file.DateModified = file.LastWriteTime;
                                SearchResult.Add(_file);
                            }
                            else
                                if (File.ToLower().Contains(SearchForText))
                                {
                                    FileInfo file = new FileInfo(File);
                                    File _file = new File();
                                    _file.Name = file.Name;
                                    _file.Info = file;
                                    _file.FileType = file.Extension;
                                    _file.Size = file.Length / 1024;
                                    _file.Icon = ExtractIcon(file);
                                    _file.DateModified = file.LastWriteTime;
                                    SearchResult.Add(_file);
                                }
                        }
                        else
                            if (File.ToLower().Contains(SearchForText))
                            {
                                FileInfo file = new FileInfo(File);
                                File _file = new File();
                                _file.Name = file.Name;
                                _file.Info = file;
                                _file.FileType = file.Extension;
                                _file.Size = file.Length / 1024;
                                _file.Icon = ExtractIcon(file);
                                _file.DateModified = file.LastWriteTime;
                                SearchResult.Add(_file);
                            }

                    }
                }
            }
            catch (Exception)
            {
            }

            // Recursively add all the files in the
            // current directory's subdirectories.
            try
            {
                foreach (string D in Directory.GetDirectories(Dir))
                {
                    GetFiles(RootDir, D, FileExtensions, SearchForText, CaseSensitive, UseRegularExpression, UseIFilter, countVisitDepth);
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// get the file content
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        public String GetFileContent(String FileName, out bool Error)
        {
            String TextContent = "";

            FileInfo fInfo = new FileInfo(FileName);

            FileStream fStream = null;
            StreamReader sReader = null;

            Error = false;

            try
            {
                fStream = fInfo.OpenRead();
                sReader = new StreamReader(fStream);

                TextContent = sReader.ReadToEnd();


            }
            catch (System.IO.IOException)
            {
                Error = true;
            }
            finally
            {
                if (fStream != null)
                {
                    fStream.Close();
                }

                if (sReader != null)
                {
                    sReader.Close();
                }

            }

            return TextContent;
        }



        /// <summary>
        /// search in a PDF file
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="SearchForText"></param>
        /// <param name="CaseSensitive"></param>
        /// <param name="?"></param>
        /// <param name="UseRegularExpression"></param>
        /// <returns></returns>
        public bool SearchInPdf(String FileName, String SearchForText, bool CaseSensitive, bool UseRegularExpression)
        {
            bool Result = false;
            PdfReader reader = new PdfReader(FileName);


            for (int page = 1; page <= reader.NumberOfPages; page++)
            {
                string pageContent = "";
                // get pdf page content
                byte[] pdf = reader.GetPageContent(page);

                bool inp = false;

                // get only texts
                // discard all the rest
                for (int i = 0; i < pdf.Length; i++)
                {
                    char s = Convert.ToChar(pdf[i]);

                    if (s == '(')
                    {
                        inp = true;
                    }
                    else if (inp)
                    {
                        if (s == ')')
                        {
                            inp = false;
                        }
                        else
                        {
                            if (Char.IsLetterOrDigit(s) || (Char.IsWhiteSpace(s)) || (Char.IsPunctuation(s)))
                                pageContent += s;
                        }
                    }
                }

                Result = containsPattern(SearchForText, CaseSensitive, UseRegularExpression, pageContent);
                if (Result)
                    break;

            }
            return Result;
        }

        /// <summary>
        /// if SearchForText length i > 0
        /// search it inside the file content
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="SearchForText"></param>
        /// <param name="CaseSensitive"></param>
        /// <param name="UseRegularExpression"></param>
        /// <param name="UseIFilter"></param>
        /// <returns></returns>
        public bool FileContainsText(String FileName, String SearchForText, bool CaseSensitive, bool UseRegularExpression, bool UseIFilter)
        {
            bool Result = (SearchForText.Length == 0);

            if (!Result)
            {

                // try to use IFilter if you have checked
                // UseIFilter checkbox
                //if (Parser.IsParseable(FileName) && UseIFilter)
                //{
                //    string content = Parser.Parse(FileName);
                //    // if content length > 0
                //    // means that IFilter works and returns the file content
                //    // otherwise IFilter hadn't read the file content
                //    // i.e. IFilter seems no to be able to extract text contained in dll or exe file
                //    if (content.Length > 0)
                //    {
                //        Result = containsPattern(SearchForText, CaseSensitive, UseRegularExpression, content);
                //        return Result;
                //    }
                //}
                // scan files to get plaintext
                // with my routines
                if (FileName.ToLower().EndsWith(".pdf"))
                {
                    // search text in a pdf file
                    Result = SearchInPdf(FileName, SearchForText, CaseSensitive, UseRegularExpression);
                }
                else
                {
                    bool Error;
                    String TextContent = GetFileContent(FileName, out Error);

                    if (!Error)
                    {
                        Result = containsPattern(SearchForText, CaseSensitive, UseRegularExpression, TextContent);
                    }
                }
            }

            return Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SearchForText"></param>
        /// <param name="CaseSensitive"></param>
        /// <param name="UseRegularExpression"></param>
        /// <param name="TextContent"></param>
        private bool containsPattern(String SearchForText, bool CaseSensitive, bool UseRegularExpression, String TextContent)
        {
            bool Result = false;

            if (UseRegularExpression)
            {
                RegexOptions options = RegexOptions.Multiline | RegexOptions.CultureInvariant;
                if (!CaseSensitive)
                    options = options | RegexOptions.IgnoreCase;
                Result = System.Text.RegularExpressions.Regex.IsMatch(TextContent, SearchForText, options);
            }
            else
            {
                if (!CaseSensitive)
                {
                    TextContent = TextContent.ToUpper();
                    SearchForText = SearchForText.ToUpper();
                }

                Result = (TextContent.IndexOf(SearchForText) != -1);
            }
            return Result;
        }


        public string Dir { get; set; }

        public string SearchForText { get; set; }

        public bool CaseSensitive { get; set; }

        public bool UseRegularExpression { get; set; }

        public bool UseIFilter { get; set; }

        public string[] FileExtensions { get; set; }
    }
}
