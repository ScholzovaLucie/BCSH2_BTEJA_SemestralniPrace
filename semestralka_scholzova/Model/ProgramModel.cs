using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls;
using semestralka_scholzova.Model;

namespace semestralka_scholzova.Model
{
    public class PreogramModel{

       }
    public class Program : INotifyPropertyChanged
    {
        List<Token> tokens;
        Lexer scanner;
        Parser parser;
        public Block block;
        static bool isError = false;

        private string readText;
        private string customConsole;

        public string ReadeText
        {
            get { return readText; }

            set
            {
                if (readText != value)
                {
                    readText = value;
                    RaisePropertyChanged("ReadeText");
                }
            }
        }

        public string CustomConsole
        {
            get { return customConsole; }

            set
            {
                if (customConsole != value)
                {
                    customConsole = value;
                    RaisePropertyChanged("CustomConsole");
                }
            }

        }


        public void run()
        {
            CustomConsole = null;
            tokens = new List<Token>();

            scanner = new Lexer(readText);
            tokens = scanner.ScanTokens();

            parser = new Parser(tokens);
            block = parser.Parse();

            //Prepare();
            //Thread threadUI = new Thread(() =>
            //{
            CustomConsole = block.execute(CustomConsole);

            //});
            //threadUI.Start();

           
        }

        public static void Error(int line, string message)
        {

            isError = true;
        }
        public static void Error(string message)
        {

            isError = true;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }



    }
}
