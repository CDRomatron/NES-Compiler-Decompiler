using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using NESCompilerDecompiler.FileReader;

namespace NESCompilerDecompiler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnComp_Click(object sender, RoutedEventArgs e)
        {

            string defaultExtensionH = "nesh";
            string[] filtersH = { "nesh" };

            string pathH = Browse(defaultExtensionH, filtersH);

            if(pathH != "")
            {
                string defaultExtensionA = "nesa";
                string[] filtersA = { "nesa" };

                string pathA = Browse(defaultExtensionA, filtersA);

                if(pathA != "")
                {

                    string defaultExtension = "nes";
                    string[] filters = { "nes" };

                    string pathSave = Save(defaultExtension, filters);

                    if(pathSave != "")
                    {
                        TextReader tr = new TextReader();

                        tr.ReadFile(pathH, pathA, pathSave);

                        MessageBox.Show("Done!");
                    }
                    else
                    {
                        MessageBox.Show("Error, no save file chosen.");
                    }
                }
                else
                {
                    MessageBox.Show("Error, no assembly file chosen.");
                }
            }
            else
            {
                MessageBox.Show("Error, no header file chosen.");
            }

        }

        private void btnDecom_Click(object sender, RoutedEventArgs e)
        {
            string defaultExtension = "nes";
            string[] filters = { "nes" };

            string path = Browse(defaultExtension, filters);

            if (path != "")
            {
                HexReader hr = new HexReader();

                hr.ReadFile(path);

                MessageBox.Show("Done!");
            }
            else
            {
                MessageBox.Show("Error, no file chosen.");
            }

        }

        /*
         * Opens file dialog, when provded with a default extension, and a array of filters. Returns the path of file chosen by the dialog box.
         */
        private string Browse(string defaultExtension, string[] filters)
        {
            string path = "";

            OpenFileDialog dlg = new OpenFileDialog();

            dlg.DefaultExt = defaultExtension;

            string filterText = "";

            for(int i = 0; i < filters.Length; i++)
            {
                filterText += filters[i].ToUpper() + " Files (*." + filters[i] + ")|*." + filters[i];

                if (i < filters.Length - 1)
                {
                    filterText += "|";
                }
            }

            dlg.Filter = filterText;

            Nullable<bool> result = dlg.ShowDialog();

            if(result == true)
            {
                path = dlg.FileName;
            }

            return path;
        }

        private string Save(string defaultExtension, string[] filters)
        {
            string path = "";

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "Compiled Rom";
            dlg.DefaultExt = defaultExtension;

            string filterText = "";

            for (int i = 0; i < filters.Length; i++)
            {
                filterText += filters[i].ToUpper() + " Files (*." + filters[i] + ")|*." + filters[i];

                if (i < filters.Length - 1)
                {
                    filterText += "|";
                }
            }

            dlg.Filter = filterText;

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                path = dlg.FileName;
            }

            return path;
        }
    }
}
