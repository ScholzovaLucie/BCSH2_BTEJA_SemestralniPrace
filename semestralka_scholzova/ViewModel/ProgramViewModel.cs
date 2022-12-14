using semestralka_scholzova.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace semestralka_scholzova.ViewModel
{
    class ProgramViewModel
    {
        public MyICommand RunCommand { get; set; }
        public MyICommand ImportCommand { get; set; }
        public MyICommand SaveCommand { get; set; }
        public MyICommand ClearComannd { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private Perzistence per = new Perzistence();
        public UserControl MainWindow { get; }
        public Program program = new Program();
        Thread threadUI;

        public ProgramViewModel()
        {
            
            RunCommand = new MyICommand(RunClick);
            ImportCommand = new MyICommand(ImportClick);
            SaveCommand = new MyICommand(SaveClick);
            ClearComannd = new MyICommand(ClearConsole);
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

            threadUI = new Thread(() =>
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

    

        private void ClearConsole()
        {
            program.CustomConsole = "";
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
