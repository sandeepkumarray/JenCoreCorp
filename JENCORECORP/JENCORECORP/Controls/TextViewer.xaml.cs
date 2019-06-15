using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace JENCORECORP
{
    /// <summary>
    /// Interaction logic for TextViewer.xaml
    /// </summary>
    public partial class TextViewer : UserControlClass
    {
        public TextViewer()
        {
            InitializeComponent();
        }

        public TextViewer(String Data)
        {
            InitializeComponent();
            string ext = string.Empty;
            try
            {
                ext = System.IO.Path.GetExtension(Data);
            }
            catch (ArgumentException) { }
            catch (System.IO.PathTooLongException) { }
            catch (NotSupportedException) { }

            if (ext == ".rtf")
            {
                byte[] xpsBytes = System.IO.File.ReadAllBytes(Data);
                using (var reader = new MemoryStream(xpsBytes))
                {
                    reader.Position = 0;
                    Rtbox.SelectAll();
                    Rtbox.Selection.Load(reader, DataFormats.Rtf);
                }
            }
            else
            {
                Rtbox.AppendText(Data);
            }

        }
    }
}
