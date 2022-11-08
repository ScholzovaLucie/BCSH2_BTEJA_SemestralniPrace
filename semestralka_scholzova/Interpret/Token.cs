using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Interpret
{
    public class Token
    {
        public EnumTokens type;
        public string lexeme;
        public int line;
        public object literal;

        public Token(EnumTokens type, string lexeme, int line, object literal)
        {
            this.type = type;
            this.lexeme = lexeme;
            this.line = line;
            this.literal = literal;
        }

        public String ToString()
        {
            return type + " " + lexeme;
        }
    }
}
