using semestralka_scholzova.Model;
using semestralka_scholzova.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace semestralka_scholzova.Views
{
    /// <summary>
    /// Interakční logika pro ProgramView.xaml
    /// </summary>
    public partial class ProgramView : UserControl
    {
               
      public ProgramView() {
            InitializeComponent();
            this.DataContext = new ViewModel.ProgramViewModel();
        }
      


    }
}
