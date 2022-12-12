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

namespace semestralka_scholzova.Views
{
    /// <summary>
    /// Interakční logika pro ProgramView.xaml
    /// </summary>
    public partial class ProgramView : UserControl
    {
        private AlgorithmBase algorithm = new AlgorithmBase();
       
        public MyICommand RunCommand { get; set; }
        public MyICommand ImportCommand { get; set; }
        public MyICommand SaveCommand { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        private Perzistence per = new Perzistence();
        public UserControl MainWindow { get; }
        public Program program = new Program();

        public ProgramView()
        {
            InitializeComponent();
            RunCommand = new MyICommand(RunClick);
            ImportCommand = new MyICommand(ImportClick);
            SaveCommand = new MyICommand(SaveClick);
            LoadProgram();
        }

        private void ImportClick()
        {
            program.ReadeText = per.Import(program);

        }
        private void SaveClick()
        {
            per.Save(program.ReadeText, program);

        }

        private void RunClick()
        {

            Thread threadUI = new Thread(() =>
            {
                if (program.ReadeText != null)
                {
                    program.run();

            }
            });
            threadUI.SetApartmentState(ApartmentState.STA);
            threadUI.IsBackground = true;
            threadUI.Start();
        }



        public ObservableCollection<Program> Programs
        {
            get;
            set;
        }

        public void LoadProgram()
        {
            ObservableCollection<Program> programs = new ObservableCollection<Program>();

            programs.Add(program);

            Programs = programs;
        }



    }
}
