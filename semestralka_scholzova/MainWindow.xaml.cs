using Microsoft.Win32;
using semestralka_scholzova.Model;
using semestralka_scholzova.ViewModel;
using semestralka_scholzova.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
using Path = System.IO.Path;

namespace semestralka_scholzova
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

        private void ProgramViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            ProgramViewModel programViewModelObject =
               new ProgramViewModel();
            programViewModelObject.LoadProgram();

            ProgramViewControl.DataContext = programViewModelObject;
        }

        

       



    }
}
