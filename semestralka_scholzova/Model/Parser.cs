
using semestralka_scholzova;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Model
{
    public class Parser
    {
        public List<Token> tokens = new List<Token>();
        public int index = 0;
        Block block = new Block();
        UserException exception;
        bool chyba = false;

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public Block Parse()
        {
            block = ReadBlock();
            return block;
        }

        public Block ReadBlock()
        {
            while (index < tokens.Count - 1)
            {
                if (chyba == true) goto nastalachyba;
                if (tokens[index].type == EnumTokens.LET)
                {
                    Statement st;
                    Let let;
                    (let, st) = ReadVar();
                    block.vars.Add(let);
                    block.Statements.Add(st);
                }

                else if (tokens[index].type == EnumTokens.FUNCTION) block.Functions.Add(ReadeFunction());
                else if (tokens[index].type == EnumTokens.VARIABLE) block.Statements.Add(setVariable());
                else if (tokens[index].type == EnumTokens.IF) block.Statements.Add(ReadIfStatement(""));
                else if (tokens[index].type == EnumTokens.WHILE) block.Statements.Add(ReadWhileStatement(""));
                else if (tokens[index].type == EnumTokens.WRITE) block.Statements.Add(ReadWriteStatement());

                else goto nastalachyba;
            }
            nastalachyba:
            return block;
        }

        private (Let, Statement) ReadVar()
        {
            Let newVar;

            if (tokens[index++].type != EnumTokens.LET) { exception = new UserException(); chyba = true; return (null, null); }
            if (tokens[index].type == EnumTokens.VARIABLE) { }
            else exception = new UserException();
            Token ident = tokens[index++];

            if (tokens[index++].type != EnumTokens.COLON) { exception = new UserException(); chyba = true; return (null, null); }

            if (tokens[index].type == EnumTokens.INT) { }
            else if (tokens[index].type == EnumTokens.FLOAT) { }
            else if (tokens[index].type == EnumTokens.BOOLEAN) { }
            else if (tokens[index].type == EnumTokens.STRING) { }
            else { exception = new UserException(); return (null, null); }

            Token type = tokens[index++];
            index++;
            if (tokens[index].type == EnumTokens.READ)
            {
                newVar = new Let(ident.literal, type.literal);
                return (newVar, ReadReadStatement(ident));
            }
            if (tokens[index].type == EnumTokens.LEFT_PAREN)
            {
                Statement st;
                index++;
                switch (tokens[index].type)
                {
                    case EnumTokens.INT:
                        st = INTRetyping();
                        break;

                    case EnumTokens.FLOAT:
                        st = FLOATRetyping();
                        break;

                    case EnumTokens.STRING:
                        st = STRINGRetyping();
                        break;
                    default:
                        st = null;
                        break;
                }
                index++;

                if (tokens[index++].type != EnumTokens.SEMICOLON) { exception = new UserException(); chyba = true; return (null, null); }
                newVar = new Let(ident.literal, type.literal);
                return (newVar, st);

            }
            Expression ex = Expression();
            SetStatement setstmp = new SetStatement(ident, type.literal, ex);

            if (tokens[index++].type != EnumTokens.SEMICOLON) { exception = new UserException(); chyba = true; return (null, null); }

            newVar = new Let(ident.literal, type.literal);
            return (newVar, setstmp);

        }

        private Function ReadeFunction()
        {
            UserException exception;
            Token FunctionType;
            List<Let> para = new List<Let>();

            if (tokens[index++].type != EnumTokens.FUNCTION) { exception = new UserException(); chyba = true; return null; }
            if (tokens[index].type != EnumTokens.VARIABLE) { exception = new UserException(); chyba = true; return null; }
            Token ident = tokens[index++];
            if (tokens[index++].type != EnumTokens.LEFT_PAREN) { exception = new UserException(); chyba = true; return null; }
            if (tokens[index].type == EnumTokens.VARIABLE)
            {
                Token par = tokens[index++];
                if (tokens[index++].type != EnumTokens.COLON) { exception = new UserException(); chyba = true; return null; }
                if (tokens[index].type == EnumTokens.INT) { }
                else if (tokens[index].type == EnumTokens.FLOAT) { }
                else if (tokens[index].type == EnumTokens.BOOLEAN) { }
                else if (tokens[index].type == EnumTokens.VOID) { }
                else if (tokens[index].type == EnumTokens.STRING) { }
                else { exception = new UserException("Expected Type"); }
                Token type = tokens[index++];

                para.Add(new Let(par.lexeme, type.literal, null));

                while (tokens[index].type != EnumTokens.RIGHT_PAREN)
                {
                    if (tokens[index++].type != EnumTokens.COMMA) { exception = new UserException(); chyba = true; return null; }
                    if (tokens[index].type != EnumTokens.VARIABLE) { exception = new UserException(); chyba = true; return null; }
                    Token par2 = tokens[index++];
                    if (tokens[index++].type != EnumTokens.COLON) { exception = new UserException(); chyba = true; return null; }
                    if (tokens[index].type == EnumTokens.INT) { }
                    else if (tokens[index].type == EnumTokens.FLOAT) { }
                    else if (tokens[index].type == EnumTokens.BOOLEAN) { }
                    else if (tokens[index].type == EnumTokens.STRING) { }
                    else { exception = new UserException("Expected Type"); }
                    Token type2 = tokens[index++];
                    para.Add(new Let(par2.lexeme, type2.literal, null));
                }
                if (tokens[index].type != EnumTokens.RIGHT_PAREN) { exception = new UserException(); chyba = true; return null; }
                }
            if (tokens[index++].type != EnumTokens.RIGHT_PAREN) { exception = new UserException(); chyba = true; return null; }
                if (tokens[index++].type != EnumTokens.COLON) { exception = new UserException("Expected COLON"); chyba = true; return null; }
                if (tokens[index].type == EnumTokens.INT) { }
            else if (tokens[index].type == EnumTokens.FLOAT) { }
            else if (tokens[index].type == EnumTokens.BOOLEAN) { }
            else if (tokens[index].type == EnumTokens.VOID) { }
            else { exception = new UserException("Expected Function Type"); chyba = true; return null; }
            FunctionType = tokens[index++];
            if (tokens[index++].type != EnumTokens.LEFT_BRACE) { exception = new UserException("Expected LEFT_BRACE"); chyba = true; return null; }
                FunctionStatement st = ReadeFunctionStatement(ident.lexeme);

            Function fun = new Function(ident.lexeme, FunctionType);
            fun.parameters = para;
            fun.Statements = st.stmp;
            fun.Functions = st.functions;
            fun.vars = st.variables;
            if (FunctionType.type == EnumTokens.INT || FunctionType.type == EnumTokens.FLOAT || FunctionType.type == EnumTokens.BOOLEAN || FunctionType.type == EnumTokens.STRING)
            {
                if (!st.GetList().OfType<ReturnStatement>().Any())
                {
                    exception = new UserException();
                }
            }

            return fun;

        }

        private FunctionStatement ReadeFunctionStatement(string ident)
        {
            List<Statement> stmp = new List<Statement>();
            List<Let> variables = new List<Let>();
            List<Function> functions = new List<Function>();

            while (tokens[index].type != EnumTokens.RIGHT_BRACE)
            {
                if (tokens[index].type == EnumTokens.LET)
                {
                    Statement st;
                    Let let;
                    (let, st) = ReadVar();
                    variables.Add(let);
                    stmp.Add(st);

                }

                else if (tokens[index].type == EnumTokens.FUNCTION) functions.Add(ReadeFunction());

                else if (tokens[index].type == EnumTokens.WHILE) stmp.Add(ReadWhileStatement(ident));

                else if (tokens[index].type == EnumTokens.IF) stmp.Add(ReadIfStatement(ident));

                else if (tokens[index].type == EnumTokens.WRITE) stmp.Add(ReadWriteStatement());

                else if (tokens[index].type == EnumTokens.VARIABLE) stmp.Add(setVariable());

                else if (tokens[index].type == EnumTokens.RETURN) stmp.Add(ReadeReturnStatement(ident));


                else if(tokens[index].type == EnumTokens.CONTINUE)
                {
                    stmp.Add(new ContinueStatemant());
                    index++;
                    if (tokens[index++].type != EnumTokens.SEMICOLON)
                    {
                        exception = new UserException("Expected SEMICOLON");
                        chyba = true;

                    }
                }

                else if (tokens[index].type == EnumTokens.BREAK)
                {
                    stmp.Add(new BreakeStatemant());
                    index++;
                    if (tokens[index++].type != EnumTokens.SEMICOLON)
                    {
                        exception = new UserException("Expected SEMICOLON");
                        chyba = true;

                    }
                }
            }

            if (tokens[index++].type != EnumTokens.RIGHT_BRACE) { exception = new UserException("Expected RIGHT_BRACE"); chyba = true; return null; }



            return new FunctionStatement(stmp, variables, functions);
        }

        public Statement ReadeReturnStatement(string ident)
        {
  
            Token token = tokens[index++];
            if (nasledujici() == "(")
            {
                ReturnStatement st = new ReturnStatement(new CallExpression(tokens[index++]), ident);
                if (tokens[index++].type != EnumTokens.SEMICOLON) { exception = new UserException(); chyba = true; return null; }
                return st;
            }
            else
            {
                ReturnStatement st = new ReturnStatement(Expression(), ident);
                if (tokens[index++].type != EnumTokens.SEMICOLON) { exception = new UserException(); chyba = true; return null; }
                return st;
            }

            exception = new UserException("Expected RETURN");

        }


        public Statement ReadReadStatement(Token ident)
        {
            UserException exception;
            if (tokens[index].type == EnumTokens.READ)
            {
                ReadeStatement st = new ReadeStatement(ident);
                index++;
                if (tokens[index++].type != EnumTokens.LEFT_PAREN) { exception = new UserException(); chyba = true; return null; }
                if (tokens[index++].type != EnumTokens.RIGHT_PAREN) {exception = new UserException(); chyba = true; return null; }
                if (tokens[index++].type != EnumTokens.SEMICOLON) { exception = new UserException(); chyba = true; return null; }
                return st;
            }
            else { exception = new UserException("Expected READE"); return null; }
        }

        public Statement ReadWriteStatement()
        {
            UserException exception;
            if (tokens[index++].type == EnumTokens.WRITE)
            {
                if (tokens[index++].type != EnumTokens.LEFT_PAREN) { exception = new UserException("Expected LEFT_PAREN"); chyba = true; return null; }

                if (tokens[index].type == EnumTokens.VARIABLE)
                {
                    Statement st = new WriteStatement(Expression());
                    if (tokens[index++].type != EnumTokens.RIGHT_PAREN) { exception = new UserException("Expected RIGHT_PAREN"); chyba = true; return null; }
                    if (tokens[index++].type != EnumTokens.SEMICOLON) { exception = new UserException("Expected SEMICOLON"); chyba = true; return null; }
                    return st;
                }

                else if (tokens[index].type == EnumTokens.STRING)
                {
                    Statement st = new WriteStatement(new StringExpression(tokens[index++].lexeme));
                    if (tokens[index++].type != EnumTokens.RIGHT_PAREN) { exception = new UserException("Expected RIGHT_PAREN"); chyba = true; return null; }
                    if (tokens[index++].type != EnumTokens.SEMICOLON) { exception = new UserException("Expected SEMICOLON"); chyba = true; return null; }
                    return st;

                }
                else { exception = new UserException("Expected WRITE"); chyba = true; return null; }

            }
            else { exception = new UserException("Expected WRITE"); chyba = true; return null; }


        }


        private Statement setVariable()
        {
            Token idnet = tokens[index];
            if (tokens[index++].type == EnumTokens.VARIABLE)
            {
                if (tokens[index++].type == EnumTokens.EQUAL)
                {
                    if (tokens[index].type == EnumTokens.VARIABLE)
                    {
                        Statement st = new SetStatement(idnet, idnet.lexeme, Expression());

                        if (tokens[index++].type != EnumTokens.SEMICOLON) { exception = new UserException(); chyba = true; return null; }
                        return st;
                    }
                    if (tokens[index].type == EnumTokens.RANDOM)
                    {
                        Statement st = new SetStatement(idnet, idnet.lexeme, Expression());

                        if (tokens[index++].type != EnumTokens.SEMICOLON) { exception = new UserException(); chyba = true; return null; }
                        return st;
                    }
                    if (tokens[index].type == EnumTokens.READ)
                    {

                    }
                    Token vare = tokens[index++];
                    if (tokens[index].type == EnumTokens.SEMICOLON)
                    {
                        index--;
                        Statement st = new SetStatement(idnet, idnet.type, Expression());
                        if (tokens[index++].type != EnumTokens.SEMICOLON) { exception = new UserException(); chyba = true; return null; }

                            return st;
                    }

                    if (tokens[index].type == EnumTokens.LEFT_PAREN)
                    {
                        index++;
                        if (tokens[index++].type != EnumTokens.RIGHT_PAREN) { exception = new UserException(); chyba = true; return null; }
                        if (tokens[index++].type != EnumTokens.SEMICOLON) { exception = new UserException(); chyba = true; return null; }
                        return new SetStatement(idnet, idnet.type, new CallExpression(vare));
                    }

                    if (tokens[index].type == EnumTokens.BOOLEAN)
                    {
                        Statement st = new SetStatement(vare, EnumTokens.BOOLEAN, Expression());
                        if (tokens[index++].type != EnumTokens.SEMICOLON) { exception = new UserException(); chyba = true; return null; }
                            return st;

                    }

                }

                else
                {
                    index--;
                    index--;
                    Token vare = tokens[index++];
                    List<object> list = new List<object>();
                    if (tokens[index++].type != EnumTokens.LEFT_PAREN) { exception = new UserException(); chyba = true; return null; }
                        EnumTokens[] types = { EnumTokens.VARIABLE, EnumTokens.INT, EnumTokens.STRING, EnumTokens.BOOLEAN, EnumTokens.FLOAT };
                    if (COntainsParameter(types))
                    {

                        Token par = tokens[index++];
                        list.Add(par.literal);



                        while (tokens[index].type != EnumTokens.RIGHT_PAREN)
                        {
                            index++;

                            Token par2 = tokens[index++];
                            list.Add(par2.literal);

                        }
                    }
                    if (tokens[index++].type != EnumTokens.RIGHT_PAREN) { exception = new UserException(); chyba = true; return null; }
                    if (tokens[index++].type != EnumTokens.SEMICOLON) { exception = new UserException(); chyba = true; return null; }
                        CallStatement call = new CallStatement(vare);
                    call.parameters = list;
                    return call;


                }

            }

            exception = new UserException();
            return null;
        }

        private bool COntainsParameter(EnumTokens[] types)
        {
            if (index != tokens.Count)
            {
                foreach (var type in types)
                {
                    if (type == tokens[index].type) return true;
                }
            }
            return false;
        }

        public Statement ReadIfStatement(string ident)
        {
            if (tokens[index++].type != EnumTokens.IF) { exception = new UserException("Expected IF"); chyba = true; return null; }
            if (tokens[index++].type != EnumTokens.LEFT_PAREN) { exception = new UserException("Expected LEFT_PAREN"); chyba = true; return null; }

            Condition con = Condition();
            if (tokens[index++].type != EnumTokens.RIGHT_PAREN) { exception = new UserException(); chyba = true; return null; }
            if (tokens[index++].type != EnumTokens.LEFT_BRACE) { exception = new UserException("Expected LEFT_BRACE"); chyba = true; return null; }
            List<Statement> stmps;
            List<ElseIfStatement> elseifstmp;
            Statement elsestmp;

            (stmps, elseifstmp, elsestmp) = ReadIfFunctionStatement(ident);

            return new IfStatement(stmps, con, elseifstmp, elsestmp);
        }

        private (List<Statement>, List<ElseIfStatement>, Statement) ReadIfFunctionStatement(string ident)
        {
            List<Statement> stmp = new List<Statement>();
            List<ElseIfStatement> elseifstmp = new List<ElseIfStatement>();
            Statement elsestmp = null;
            List<Let> variables = new List<Let>();
            List<Function> functions = new List<Function>();

            while (tokens[index].type != EnumTokens.RIGHT_BRACE)
            {
                if (tokens[index].type == EnumTokens.LET)
                {
                    Statement st;
                    Let let;
                    (let, st) = ReadVar();
                    variables.Add(let);
                    stmp.Add(st);
                }

                if (tokens[index].type == EnumTokens.FUNCTION) functions.Add(ReadeFunction());

                if (tokens[index].type == EnumTokens.WHILE) stmp.Add(ReadWhileStatement(ident));

                if (tokens[index].type == EnumTokens.IF) stmp.Add(ReadIfStatement(ident));

                if (tokens[index].type == EnumTokens.WRITE) stmp.Add(ReadWriteStatement());

                if (tokens[index].type == EnumTokens.VARIABLE) stmp.Add(setVariable());

                if (tokens[index].type == EnumTokens.RETURN) stmp.Add(ReadeReturnStatement(ident));

                if (tokens[index].type == EnumTokens.CONTINUE) {
                    stmp.Add(new ContinueStatemant()); 
                    index++; 
                    if (tokens[index++].type != EnumTokens.SEMICOLON) {
                        exception = new UserException("Expected SEMICOLON"); 
                        chyba = true; 
                        
                    } 
                }

                if (tokens[index].type == EnumTokens.BREAK) {
                    stmp.Add(new BreakeStatemant()); 
                    index++;
                    if (tokens[index++].type != EnumTokens.SEMICOLON) { 
                        exception = new UserException("Expected SEMICOLON"); 
                        chyba = true; 
                  
                    } 
                }
            }

            if (tokens[index++].type != EnumTokens.RIGHT_BRACE) { exception = new UserException("Expected RIGHT_BRACE"); chyba = true; return (null, null, null); }

                if (tokens[index].type == EnumTokens.ELSEIF)
            {
                while (tokens[index].type != EnumTokens.ELSE) elseifstmp.Add(ReadeElseIfStatement(ident));
            }

            if (tokens[index].type == EnumTokens.ELSE) elsestmp = ReadeElseStatement(ident);

            return (stmp, elseifstmp, elsestmp);
        }

        private ElseIfStatement ReadeElseIfStatement(string ident)
        {
            if (tokens[index++].type != EnumTokens.ELSEIF) { exception = new UserException(); chyba = true; return null; }
            if (tokens[index++].type != EnumTokens.LEFT_PAREN) { exception = new UserException(); chyba = true; return null; }
            Condition con = Condition();
            if (tokens[index++].type != EnumTokens.RIGHT_PAREN) { exception = new UserException(); chyba = true; return null; }
            if (tokens[index++].type != EnumTokens.LEFT_BRACE) { exception = new UserException(); chyba = true; return null; }

            Statement stmp = ReadeFunctionStatement(ident);

            return new ElseIfStatement(stmp, con);
        }

        private Statement ReadeElseStatement(string ident)
        {
            if (tokens[index++].type != EnumTokens.ELSE) { exception = new UserException("Expected ELSE"); return null; }

            if (tokens[index++].type != EnumTokens.LEFT_BRACE) { exception = new UserException("Expected LEFT_BRACE"); return null; }
            Statement stmp = ReadeFunctionStatement(ident);

            return new ElseStatement(stmp);
        }

        public Statement ReadWhileStatement(string ident)
        {
            if (tokens[index++].type != EnumTokens.WHILE) { exception = new UserException("Expected WHILE"); chyba = true; return null; }
            if (tokens[index++].type != EnumTokens.LEFT_PAREN) { exception = new UserException("Expected LEFT_PAREN"); chyba = true; return null; }
            Condition con = Condition();
            if (tokens[index++].type != EnumTokens.RIGHT_PAREN) { exception = new UserException(); chyba = true; return null; }
                if (tokens[index++].type != EnumTokens.LEFT_BRACE) { exception = new UserException("Expected LEFT_BRACE"); chyba = true; return null; }

            Statement stmnt = ReadeFunctionStatement(ident);

            return new WhileStatement(stmnt, con);
        }

        private Condition Condition()
        {
            Condition cond = null;
            Expression expr = Expression();
            EnumTokens[] types = { EnumTokens.EQUAL_EQUAL, EnumTokens.GREATER, EnumTokens.GREATER_EQUAL, EnumTokens.LESS, EnumTokens.LESS_EQUAL, EnumTokens.ODD, EnumTokens.HASHTAG, EnumTokens.BANG_EQUAL };
            while (ContainsAktualToken(types))
            {
                Token operatorVar = tokens[index++];
                Expression right = Expression();

                if (operatorVar.type.Equals(EnumTokens.ODD)) cond = new OddCondition(right);
                else cond = new BinaryCondition(expr, operatorVar, right);
            }

            return cond;
        }

        private Expression Expression()
        {
            Expression expr = ReadBinary();
            EnumTokens[] types = { EnumTokens.MINUS, EnumTokens.PLUS };
            while (ContainsAktualToken(types)) expr = new BinaryExpression(expr, tokens[index++], ReadBinary());

            return expr;
        }

        private Expression ReadBinary()
        {
            Expression expr = ReadUnary();
            EnumTokens[] types = { EnumTokens.SLASH, EnumTokens.STAR };
            while (ContainsAktualToken(types)) expr = new BinaryExpression(expr, tokens[index++], ReadUnary());

            return expr;
        }

        private Expression ReadUnary()
        {
            Expression expres;
            if (index != tokens.Count && tokens[index].type == EnumTokens.MINUS)
            {
                expres = new UnaryExpression(tokens[index++], ReadUnary());
                return expres;
            }

            else return ReadLiteral();
        }

        private Expression ReadLiteral()
        {
            Expression expres = null;
            if (index != tokens.Count && tokens[index].type == EnumTokens.INT)
            {
                expres = new LiteralExpression(tokens[index++].literal);
                return expres;
            }
            else if (index != tokens.Count && tokens[index].type == EnumTokens.FLOAT)
            {
                expres = new LiteralExpression(tokens[index++].literal);
                return expres;
            }
            else if (index != tokens.Count && tokens[index].type == EnumTokens.ODD) return null;

            else if (index != tokens.Count && tokens[index].type == EnumTokens.VARIABLE)
            {
                Token act = tokens[index++];
                if (tokens[index].lexeme == "(")
                {
                    expres = new CallExpression(act.literal);
                    if (tokens[index++].type != EnumTokens.LEFT_PAREN) { exception = new UserException(); chyba = true; return null; }
                    if (tokens[index++].type != EnumTokens.RIGHT_PAREN) { exception = new UserException(); chyba = true; return null; }
                }
                else
                {
                    expres = new VariableExpression(act.literal);
                }
                

                return expres;
            }
            else if (index != tokens.Count && tokens[index].type == EnumTokens.STRING)
            {
                expres = new StringExpression(tokens[index++].literal);
                return expres;
            }
            else if (index != tokens.Count && tokens[index].type == EnumTokens.BOOLEAN)
            {
                return new VariableBoolean(tokens[index++].literal);
            }
            else if (index != tokens.Count && tokens[index++].type == EnumTokens.RANDOM)
            {
                if (tokens[index++].type != EnumTokens.LEFT_PAREN) { exception = new UserException(); chyba = true; return null; }
        if (tokens[index++].type != EnumTokens.RIGHT_PAREN) { exception = new UserException(); chyba = true; return null; }
        return new VariableRandom();
            }

    else { exception = new UserException(); chyba = true; return  null; }
}

        private Statement STRINGRetyping()
        {
            index++;
            if (tokens[index++].type != EnumTokens.RIGHT_PAREN) { exception = new UserException(); chyba = true; return null; }
    return new ToStringStatemant(tokens[index]);
        }

        private Statement FLOATRetyping()
        {
            index++;
            if (tokens[index++].type != EnumTokens.RIGHT_PAREN) { exception = new UserException("Expected RIGHT_PAREN"); chyba = true;  return null; }
            return new ToFloatStatemamant(tokens[index]);
        }

        private Statement INTRetyping()
        {
            index++;
            if (tokens[index++].type != EnumTokens.RIGHT_PAREN) { exception = new UserException("Expected RIGHT_PAREN"); chyba = true;  return null; }
            return new ToIntStatemant(tokens[index]);
        }

        private bool ContainsAktualToken(EnumTokens[] types)
        {

            if (index != tokens.Count)
            {
                foreach (var type in types)
                {
                    if (type == tokens[index].type) return true;
                }
            }
            return false;
        }

        private string nasledujici()
        {
            int newIndex = index;
            newIndex++;
            return tokens[newIndex].lexeme;
        }
    }
}
