using System;
using System.Collections.Generic;
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
using semestralka_scholzova.Model;

namespace semestralka_scholzova
{
    /// <summary>
    /// Interakční logika pro Input.xaml
    /// </summary>
    public partial class Input : Window
    {

        public MyICommand DoneCommand { get; set; }
        InputText text = new InputText();
        public string inputText = "";

        public Input()
        {
           
            InitializeComponent();
            DoneCommand = new MyICommand(doneClick);
        }

        private void doneClick()
        {
            inputText = text.TextArea;
            System.Windows.Threading.Dispatcher.Yield();
        }
    }
}
