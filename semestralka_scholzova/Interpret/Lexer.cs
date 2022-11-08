using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Interpret
{
    public class Lexer
    {
        public List<string> sourceGlobal = new List<String>();
        private string source;
        public List<Token> tokens = new List<Token>();
        int start = 0;
        int aktrulaPosition = 0;
        int line = 1;

        static Dictionary<string, EnumTokens> keywords = new Dictionary<string, EnumTokens>();

        public Lexer(string source)
        {
            this.source = source;

            if (keywords.Count == 0)
            {
                keywords.Add("and", EnumTokens.AND);
                keywords.Add("else", EnumTokens.ELSE);
                keywords.Add("for", EnumTokens.FOR);
                keywords.Add("if", EnumTokens.IF);
                keywords.Add("or", EnumTokens.OR);
                keywords.Add("print", EnumTokens.PRINT);
                keywords.Add("return", EnumTokens.RETURN);
                keywords.Add("this", EnumTokens.THIS);
                keywords.Add("var", EnumTokens.VAR);
                keywords.Add("while", EnumTokens.WHILE);
                keywords.Add("ident", EnumTokens.IDENT);
                keywords.Add("Function", EnumTokens.Function);
                keywords.Add("begin", EnumTokens.BEGIN);
                keywords.Add("call", EnumTokens.CALL);
                keywords.Add("then", EnumTokens.THEN);
                keywords.Add("do", EnumTokens.DO);
                keywords.Add("odd", EnumTokens.ODD);
                keywords.Add("int", EnumTokens.INT);
                keywords.Add("float", EnumTokens.FLOAT);
                keywords.Add("boolean", EnumTokens.BOOLEAN);
                keywords.Add("const", EnumTokens.CONST);
                keywords.Add("variable", EnumTokens.VARIABLE);
                keywords.Add("function", EnumTokens.FUNCTION);
                keywords.Add("let", EnumTokens.LET);
                keywords.Add("void", EnumTokens.VOID);
                keywords.Add("random", EnumTokens.RANDOM);
                keywords.Add("write", EnumTokens.WRITE);
                keywords.Add("read", EnumTokens.READ);
                keywords.Add("string", EnumTokens.STRING);
            }

        }

        public List<Token> ScanTokens()
        {

                while (!isAtEnd())
                {
                    start = aktrulaPosition;
                    RecognizeToken();
                }
                aktrulaPosition = 0;

            
            tokens.Add(new Token(EnumTokens.EOF, "", line, ""));
            return tokens;
        }

        private void RecognizeToken()
        {
            char c = advance();
            switch (c)
            {
                case '(': AddToken(EnumTokens.LEFT_PAREN); break;
                case ')': AddToken(EnumTokens.RIGHT_PAREN); break;
                case '{': AddToken(EnumTokens.LEFT_BRACE); break;
                case '}': AddToken(EnumTokens.RIGHT_BRACE); break;
                case ',': AddToken(EnumTokens.COMMA); break;
                case '.': AddToken(EnumTokens.DOT); break;
                case '-': AddToken(EnumTokens.MINUS); break;
                case '+': AddToken(EnumTokens.PLUS); break;
                case ';': AddToken(EnumTokens.SEMICOLON); break;
                case '*': AddToken(EnumTokens.STAR); break;
                case '?': AddToken(EnumTokens.QUESTION_MARK); break;
                case '#': AddToken(EnumTokens.HASHTAG); break;
                case '"': createString(); break;
                case '!': AddToken(Match('=') ? EnumTokens.BANG_EQUAL : EnumTokens.BANG); break;
                case '=': AddToken(Match('=') ? EnumTokens.EQUAL_EQUAL : EnumTokens.EQUAL); break;
                case '<': AddToken(Match('=') ? EnumTokens.LESS_EQUAL : EnumTokens.LESS); break;
                case '>': AddToken(Match('=') ? EnumTokens.GREATER_EQUAL : EnumTokens.GREATER); break;
                case ':': AddToken(Match('=') ? EnumTokens.ASSIGMENT : EnumTokens.COLON); break;
                case '/': AddToken(Match('/') ? EnumTokens.COMMENT : EnumTokens.SLASH); break;
                case ' ':
                case '\r':
                case '\t': break;
                case '\n': line++; break;
                default:
                    if (IsNumber(c)) Number();
                    else if (IsCharacter(c)) Identifier();
                    else Program.Error(line, "Unexpected character.");
                    break;
            }
        }
        private char advance()
        {
            return source[aktrulaPosition++];
        }

        private void createString()
        {
            string stringValue = "";
            while (Look() != '"')
            {
                stringValue += source[aktrulaPosition];
                aktrulaPosition++;
            }
            aktrulaPosition++;
            tokens.Add(new Token(EnumTokens.STRING, stringValue, line, stringValue));

        }

        private void AddToken(EnumTokens type)
        {
            tokens.Add(new Token(type, source.Substring(start, aktrulaPosition - start), line, source.Substring(start, aktrulaPosition - start)));
        }

        private bool Match(char expected)
        {
            if (isAtEnd()) return false;
            if (source[aktrulaPosition] != expected) return false;

            aktrulaPosition++;
            return true;
        }

        private char Look()
        {
            if (isAtEnd()) return '\0';
            char n = source[aktrulaPosition];
            return n;
            
        }

        private bool IsNumber(char c)
        {
            return c >= '0' && c <= '9';
        }

        private void Number()
        {
            while (IsNumber(Look())) aktrulaPosition++;

            bool isfloat = false;
            if (IsNumber(CheckNext()) || CheckNext().Equals(','))
            {
                if (source[aktrulaPosition].Equals(','))
                {
                    isfloat = true;
                }
                aktrulaPosition++;
                while (IsNumber(Look())) aktrulaPosition++;
            }
            if (isfloat)
            {
                tokens.Add(new Token(EnumTokens.FLOAT, source.Substring(start, aktrulaPosition - start), line, double.Parse(source.Substring(start, aktrulaPosition - start))));
            }
            else
            {
                tokens.Add(new Token(EnumTokens.INT, source.Substring(start, aktrulaPosition - start), line, double.Parse(source.Substring(start, aktrulaPosition - start))));
            }
            
        }
        private void Variable()
        {
            AddToken(EnumTokens.VARIABLE);
        }

        private char CheckNext()
        {
            if (aktrulaPosition >= source.Length) return '\0';
            return source[aktrulaPosition];
        }

        private void Identifier()
        {
            while (IsCharacter(Look()) || IsNumber(Look())) aktrulaPosition++;

            String text = source.Substring(start, aktrulaPosition - start);
            if(text == "true" || text == "false")
            {
                AddToken(EnumTokens.BOOLEAN);
            }
            else if (IsVariable(text)) Variable();
            else
            {
                EnumTokens type = keywords[text];
                if (type == EnumTokens.RANDOM)
                {
                    if(Look()!='(') type = EnumTokens.VARIABLE;
                }
                if(type == EnumTokens.ELSE)
                {
                    aktrulaPosition++;
                    if (Look() == 'i')
                    {
                        type = EnumTokens.ELSEIF;
                        while (Look() != '(') aktrulaPosition++;
                    }
                    else
                    {
                        aktrulaPosition--;
                    }
                }
                if (type == null) type = EnumTokens.VARIABLE;
                AddToken(type);
            }

        }

        private bool IsCharacter(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == '_';
        }
        private bool IsVariable(string c)
        {
            if (IsNumber(c[0]))
            {
                return false;
            }
            if (keywords.ContainsKey(c))
            {
                return false;
            }
            bool isVariable = false;
            foreach (var pis in c)
            {
                if (IsCharacter(pis))
                {
                    isVariable = true;
                }
                if (isVariable != true)
                {
                    return false;
                }

            }
            return true;
        }

        public List<Token> GetTokens()
        {
            return tokens;
        }

        private Boolean isAtEnd()
        {
            return aktrulaPosition >= source.Length;
        }
    }
}
