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
using System.IO;
using Microsoft.Win32;

namespace WpfApp9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        bool Modified { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            textbox1.Focus();
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt";
            openFileDialog1.Filter = "Text files (*.txt)|*.txt";
            bool TextSaved()
            {
                if (Modified)
                    switch (MessageBox.Show("Сохранить изменения в файле?",
                        "Подтверждение", MessageBoxButton.YesNoCancel,
                        MessageBoxImage.Question))
                    {
                        case MessageBoxResult.Yes:
                            saveAs0_Executed(null, null);
                            return !Modified;
                        case MessageBoxResult.Cancel:
                            return false;
                    }
                return true;
            }
        }

        private void exit0_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
        
        void SaveToFile(string path)
        {
            File.WriteAllText(path, textbox1.Text, Encoding.Default);
            Modified = false;
        }

        private void open0_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == true)
            {
                string path = openFileDialog1.FileName;
                textbox1.Text = File.ReadAllText(path, Encoding.Default);
                Title = "Text Edit - " + path;
                saveFileDialog1.FileName = path;
            }
        }

        private void save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string path = saveFileDialog1.FileName;
            if (path=="")
                saveAs0_Executed(null,null);
            else
                SaveToFile(path);
        }

        private void saveAs0_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var oldPath = saveFileDialog1.FileName;
            saveFileDialog1.FileName = System.IO.Path.GetFileName(oldPath);
            if (saveFileDialog1.ShowDialog() == true)
            {
                string path = saveFileDialog1.FileName;
                SaveToFile(path);
                Title = "Text Editor - " + path;
            }
            else
                saveFileDialog1.FileName=oldPath;
        }

        private void exit0_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void new0_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            textbox1.Clear();
            Title = "Text Edit";
            saveFileDialog1.FileName = "";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void textbox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
