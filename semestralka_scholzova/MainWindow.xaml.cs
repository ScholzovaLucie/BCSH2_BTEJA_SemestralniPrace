using Microsoft.Win32;
using semestralka_scholzova.Model;
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
       

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ProgramViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.ProgramViewModel programViewModelObject =
               new ViewModel.ProgramViewModel();
            programViewModelObject.LoadProgram();

            ProgramViewControl.DataContext = programViewModelObject;
        }



    }
}
