using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Program(string readText)
        {
            this.readText = readText;
        }

        public List<Token> run()
        {
            
            tokens = new List<Token>();

            scanner = new Lexer(readText);
            tokens = scanner.ScanTokens();

            parser = new Parser(tokens);
            block = parser.Parse();

            return tokens;
        }





        public static void Error(int line, String message)
        {
            Console.WriteLine("Error at line " + line + ": " + message);
            isError = true;
        }
        public static void Error(String message)
        {
            Console.WriteLine("Error: " + message);
            isError = true;
        }

    }
}
