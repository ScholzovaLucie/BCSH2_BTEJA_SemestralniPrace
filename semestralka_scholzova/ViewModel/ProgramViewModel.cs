using semestralka_scholzova.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace semestralka_scholzova.ViewModel
{
    public class ProgramViewModel
    {
        public MyICommand RunCommand { get; set; }
        public MyICommand ImportCommand { get; set; }
        public MyICommand SaveCommand { get; set; }

        private AlgorithmBase algorithm = new AlgorithmBase();

        public event PropertyChangedEventHandler PropertyChanged;
        private Perzistence per = new Perzistence();
        public UserControl MainWindow { get; }
        public Program program = new Program();

        public ProgramViewModel()
        {
            RunCommand = new MyICommand(RunClick);
            ImportCommand = new MyICommand(ImportClick);
            SaveCommand = new MyICommand(SaveClick);
            LoadProgram();
        }

        private void ImportClick()
        {
           program.ReadeText = per.Import();

        }
        private void SaveClick()
        {
          per.Save(program.ReadeText);

        }
     

        private void RunClick()
        {
            //Prepare();
            //Thread threadUI = new Thread(() =>
            //{
            if (program.ReadeText != null )
            {
                program.run();
            }
            //});
            //threadUI.Start();

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
