using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace semestralka_scholzova.Interpret
{
    public class Program
    {
        List<Token> tokens;
        Lexer scanner;
        Parser parser;
        public Block block;
        static bool isError = false;
        string readText;
        TextBox CustomConsole;

        public Program(string readText, TextBox CustomConsole)
        {
            this.readText = readText;
            this.CustomConsole = CustomConsole;
        }

        public List<Token> run()
        {
            
            tokens = new List<Token>();

            scanner = new Lexer(readText);
            tokens = scanner.ScanTokens();

            parser = new Parser(tokens);
            block = parser.Parse();
            block.execute(CustomConsole);

            return tokens;
        }

        public static void Error(int line, String message)
        {
            
            isError = true;
        }
        public static void Error(String message)
        {
           
            isError = true;
        }

    }
}
