using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace semestralka_scholzova.Model
{
    public class PreogramModel{

       }
    public partial class Program : INotifyPropertyChanged
    {
        List<Token> tokens;
        Lexer scanner;
        public Parser parser;
        public Block block;
        static bool isError = false;

        private string readText;
        private string customConsole;

        private string importConsole;

        private bool editable = true;

        private string color = "{x:Null}";

        public Program()
        {
            Editable = true;
        }

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

        public string ImportConsole
        {
            get { return importConsole; }

            set
            {
                if (importConsole != value)
                {
                    importConsole = value;
                    RaisePropertyChanged("ImportConsole");
                }
            }

        }

        public bool Editable
        {
            get { return editable; }
            set
            {
                if (editable != value)
                {
                    editable = value;
                    RaisePropertyChanged("Editable");
                }
            }
        }

        public string Color
        {
            get { return color; }
            set
            {
                if (color != value)
                {
                    color = value;
                    RaisePropertyChanged("Color");
                }
            }
        }


        public void run()
        {
            ImportConsole = null;
            CustomConsole = "";
            tokens = new List<Token>();

            scanner = new Lexer(readText);
            tokens = scanner.ScanTokens();

            parser = new Parser(tokens, this);
            block = parser.Parse();

            if (parser.chyba != true) block.execute(this);
            else CustomConsole += "-> Program ukončen\n";

            ImportConsole = null;
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
