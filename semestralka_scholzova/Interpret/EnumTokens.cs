using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Interpret
{
    public enum EnumTokens
    {
        LET, LEFT_PAREN, RIGHT_PAREN, LEFT_BRACE, RIGHT_BRACE,
        COMMA, DOT, MINUS, PLUS, SEMICOLON, SLASH, STAR, QUESTION_MARK, HASHTAG,

        BANG, BANG_EQUAL,
        EQUAL, EQUAL_EQUAL,
        GREATER, GREATER_EQUAL,
        LESS, LESS_EQUAL, TEMPLATE, COLON, OPERAND,

        STRING, INT, BOOLEAN, FLOAT, STRING_IDENTIFER,

        AND, CLASS, ELSE, FALSE, FUN, FOR, IF, NIL, OR, ELSEIF,
        PRINT, RETURN, CONTINUE, BREAK, SUPER, THIS, TRUE, VAR, WHILE, IDENT, CONST,
        Function, CALL, BEGIN, END, THEN, DO, ODD, READ, WRITE, ASSIGMENT, FUNCTION, VOID, RANDOM, READE, COMMENT,

        VARIABLE,

        EOF
    }
}
