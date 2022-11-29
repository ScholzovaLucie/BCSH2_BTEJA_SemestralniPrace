using Microsoft.Win32;
using semestralka_scholzova.Interpret;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

namespace semestralka_scholzova
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Perzistence per = new Perzistence();

        public ICommand ImportCommand { get; private set; }


        public MainWindow()
        {
            InitializeComponent();

            
        }

        private void ImportClick(object sender, RoutedEventArgs e)
        {
            textArea.Text = per.Import();
          
        }
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            per.Save(textArea.Text);
    
        }
        private void RunClick(object sender, RoutedEventArgs e)
        {
            List <Token> tokens = null;
            if(textArea.Text.Length > 0)
            {
                Program progr = new Program(textArea.Text, Console);
                tokens= progr.run();
            } 
        }

        
    }
}
