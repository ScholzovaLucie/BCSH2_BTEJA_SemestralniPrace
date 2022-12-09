
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
                if (tokens[index].type == EnumTokens.LET)
                {
                    Statement st;
                    Let let;
                    (let, st) = ReadVar();
                    block.vars.Add(let);
                    block.Statements.Add(st);
                }

                if (tokens[index].type == EnumTokens.FUNCTION) block.Functions.Add(ReadeFunction());
                if (tokens[index].type == EnumTokens.VARIABLE) block.Statements.Add(setVariable());
                if (tokens[index].type == EnumTokens.IF) block.Statements.Add(ReadIfStatement(""));
                if (tokens[index].type == EnumTokens.WHILE) block.Statements.Add(ReadWhileStatement(""));
                if (tokens[index].type == EnumTokens.WRITE) block.Statements.Add(ReadWriteStatement());
            }

            return block;
        }

        private (Let, Statement) ReadVar()
        {
            Let newVar;

            if (tokens[index++].type != EnumTokens.LET) throw new Exception();

            if (tokens[index].type == EnumTokens.VARIABLE) { }
            else throw new Exception();
            Token ident = tokens[index++];

            if (tokens[index++].type != EnumTokens.COLON) throw new Exception();

            if (tokens[index].type == EnumTokens.INT) { }
            else if (tokens[index].type == EnumTokens.FLOAT) { }
            else if (tokens[index].type == EnumTokens.BOOLEAN) { }
            else if (tokens[index].type == EnumTokens.STRING) { }
            else throw new Exception();

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

                if (tokens[index++].type != EnumTokens.SEMICOLON) throw new Exception();
                newVar = new Let(ident.literal, type.literal);
                return (newVar, st);

            }
            Expression ex = Expression();
            SetStatement setstmp = new SetStatement(ident, type.literal, ex);

            if (tokens[index++].type != EnumTokens.SEMICOLON) throw new Exception();

            newVar = new Let(ident.literal, type.literal);
            return (newVar, setstmp);

        }

        private Function ReadeFunction()
        {

            Token FunctionType;
            List<Let> para = new List<Let>();

            if (tokens[index++].type != EnumTokens.FUNCTION) throw new Exception("Expected FUNCTION");
            if (tokens[index].type != EnumTokens.VARIABLE) throw new Exception("Expected VARIABLE");
            Token ident = tokens[index++];
            if (tokens[index++].type != EnumTokens.LEFT_PAREN) throw new Exception("Expected LEFT_PAREN");
            if (tokens[index].type == EnumTokens.VARIABLE)
            {
                Token par = tokens[index++];
                if (tokens[index++].type != EnumTokens.COLON) throw new Exception("Expected COLON");
                if (tokens[index].type == EnumTokens.INT) { }
                else if (tokens[index].type == EnumTokens.FLOAT) { }
                else if (tokens[index].type == EnumTokens.BOOLEAN) { }
                else if (tokens[index].type == EnumTokens.VOID) { }
                else if (tokens[index].type == EnumTokens.STRING) { }
                else { throw new Exception("Expected Type"); }
                Token type = tokens[index++];

                para.Add(new Let(par.lexeme, type.literal, null));

                while (tokens[index].type != EnumTokens.RIGHT_PAREN)
                {
                    if (tokens[index++].type != EnumTokens.COMMA) throw new Exception("Expected COMMA");
                    if (tokens[index].type != EnumTokens.VARIABLE) throw new Exception("Expected VARIABLE");
                    Token par2 = tokens[index++];
                    if (tokens[index++].type != EnumTokens.COLON) throw new Exception("Expected COLON");
                    if (tokens[index].type == EnumTokens.INT) { }
                    else if (tokens[index].type == EnumTokens.FLOAT) { }
                    else if (tokens[index].type == EnumTokens.BOOLEAN) { }
                    else if (tokens[index].type == EnumTokens.STRING) { }
                    else { throw new Exception("Expected Type"); }
                    Token type2 = tokens[index++];
                    para.Add(new Let(par2.lexeme, type2.literal, null));
                }
                if (tokens[index].type != EnumTokens.RIGHT_PAREN) throw new Exception();
            }
            if (tokens[index++].type != EnumTokens.RIGHT_PAREN) throw new Exception();
            if (tokens[index++].type != EnumTokens.COLON) throw new Exception("Expected COLON");
            if (tokens[index].type == EnumTokens.INT) { }
            else if (tokens[index].type == EnumTokens.FLOAT) { }
            else if (tokens[index].type == EnumTokens.BOOLEAN) { }
            else if (tokens[index].type == EnumTokens.VOID) { }
            else { throw new Exception("Expected Function Type"); }
            FunctionType = tokens[index++];
            if (tokens[index++].type != EnumTokens.LEFT_BRACE) throw new Exception("Expected LEFT_BRACE");
            FunctionStatement st = ReadeFunctionStatement(ident.lexeme);

            Function fun = new Function(ident, FunctionType);
            fun.parameters = para;
            fun.Statements = st.stmp;
            fun.Functions = st.functions;
            fun.vars = st.variables;
            if (FunctionType.type == EnumTokens.INT || FunctionType.type == EnumTokens.FLOAT || FunctionType.type == EnumTokens.BOOLEAN || FunctionType.type == EnumTokens.STRING)
            {
                if (!st.GetList().OfType<ReturnStatement>().Any())
                {
                    throw new Exception();
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

                else if (tokens[index].type == EnumTokens.CONTINUE) break;

                else if (tokens[index].type == EnumTokens.BREAK) break;
            }

            if (tokens[index++].type != EnumTokens.RIGHT_BRACE) throw new Exception("Expected RIGHT_BRACE");



            return new FunctionStatement(stmp, variables, functions);
        }

        public Statement ReadeReturnStatement(string ident)
        {
            Token token = tokens[index++];
            if (nasledujici() == "(")
            {
                ReturnStatement st = new ReturnStatement(new CallExpression(tokens[index++]), ident);
                if (tokens[index++].type != EnumTokens.SEMICOLON) throw new Exception();
                return st;
            }
            else
            {
                ReturnStatement st = new ReturnStatement(Expression(), ident);
                if (tokens[index++].type != EnumTokens.SEMICOLON) throw new Exception();
                return st;
            }

            throw new Exception("Expected RETURN");

        }


        public Statement ReadReadStatement(Token ident)
        {
            if (tokens[index].type == EnumTokens.READ)
            {
                ReadeStatement st = new ReadeStatement(ident);
                index++;
                if (tokens[index++].type != EnumTokens.LEFT_PAREN) throw new Exception();
                if (tokens[index++].type != EnumTokens.RIGHT_PAREN) throw new Exception();
                if (tokens[index++].type != EnumTokens.SEMICOLON) throw new Exception();
                return st;
            }
            else throw new Exception("Expected READE");
        }

        public Statement ReadWriteStatement()
        {
            if (tokens[index++].type == EnumTokens.WRITE)
            {
                if (tokens[index++].type != EnumTokens.LEFT_PAREN) throw new Exception("Expected LEFT_PAREN");

                if (tokens[index].type == EnumTokens.VARIABLE)
                {
                    Statement st = new WriteStatement(Expression());
                    if (tokens[index++].type != EnumTokens.RIGHT_PAREN) throw new Exception("Expected RIGHT_PAREN");
                    if (tokens[index++].type != EnumTokens.SEMICOLON) throw new Exception("Expected SEMICOLON");
                    return st;
                }

                else if (tokens[index].type == EnumTokens.STRING)
                {
                    Statement st = new WriteStatement(new StringExpression(tokens[index++].lexeme));
                    if (tokens[index++].type != EnumTokens.RIGHT_PAREN) throw new Exception("Expected RIGHT_PAREN");
                    if (tokens[index++].type != EnumTokens.SEMICOLON) throw new Exception("Expected SEMICOLON");
                    return st;

                }
                else throw new Exception("Expected WRITE");

            }
            else throw new Exception("Expected WRITE");


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

                        if (tokens[index++].type != EnumTokens.SEMICOLON) throw new Exception();
                        return st;
                    }
                    if (tokens[index].type == EnumTokens.RANDOM)
                    {
                        Statement st = new SetStatement(idnet, idnet.lexeme, Expression());

                        if (tokens[index++].type != EnumTokens.SEMICOLON) throw new Exception();
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
                        if (tokens[index++].type != EnumTokens.SEMICOLON) throw new Exception();

                        return st;
                    }

                    if (tokens[index].type == EnumTokens.LEFT_PAREN)
                    {
                        index++;
                        if (tokens[index++].type != EnumTokens.RIGHT_PAREN) throw new Exception();
                        if (tokens[index++].type != EnumTokens.SEMICOLON) throw new Exception();
                        return new SetStatement(idnet, idnet.type, new CallExpression(vare));
                    }

                    if (tokens[index].type == EnumTokens.BOOLEAN)
                    {
                        Statement st = new SetStatement(vare, EnumTokens.BOOLEAN, Expression());
                        if (tokens[index++].type != EnumTokens.SEMICOLON) throw new Exception();
                        return st;

                    }

                }

                else
                {
                    index--;
                    index--;
                    Token vare = tokens[index++];
                    List<object> list = new List<object>();
                    if (tokens[index++].type != EnumTokens.LEFT_PAREN) throw new Exception();
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
                    if (tokens[index++].type != EnumTokens.RIGHT_PAREN) throw new Exception();
                    if (tokens[index++].type != EnumTokens.SEMICOLON) throw new Exception();
                    CallStatement call = new CallStatement(vare);
                    call.parameters = list;
                    return call;


                }

            }

            throw new Exception();
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
            if (tokens[index++].type != EnumTokens.IF) throw new Exception("Expected IF");
            if (tokens[index++].type != EnumTokens.LEFT_PAREN) throw new Exception("Expected LEFT_PAREN");
            Condition con = Condition();
            if (tokens[index++].type != EnumTokens.RIGHT_PAREN) throw new Exception("Expected RIGHT_PAREN");
            if (tokens[index++].type != EnumTokens.LEFT_BRACE) throw new Exception("Expected LEFT_BRACE");
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

                if (tokens[index].type == EnumTokens.RETURN) break;
            }

            if (tokens[index++].type != EnumTokens.RIGHT_BRACE) throw new Exception("Expected RIGHT_BRACE");

            if (tokens[index].type == EnumTokens.ELSEIF)
            {
                while (tokens[index].type != EnumTokens.ELSE) elseifstmp.Add(ReadeElseIfStatement(ident));
            }

            if (tokens[index].type == EnumTokens.ELSE) elsestmp = ReadeElseStatement(ident);

            return (stmp, elseifstmp, elsestmp);
        }

        private ElseIfStatement ReadeElseIfStatement(string ident)
        {
            if (tokens[index++].type != EnumTokens.ELSEIF) throw new Exception("Expected ELSEIF");
            if (tokens[index++].type != EnumTokens.LEFT_PAREN) throw new Exception("Expected LEFT_PAREN");
            Condition con = Condition();
            if (tokens[index++].type != EnumTokens.RIGHT_PAREN) throw new Exception("Expected RIGHT_PAREN");
            if (tokens[index++].type != EnumTokens.LEFT_BRACE) throw new Exception("Expected LEFT_BRACE");

            Statement stmp = ReadeFunctionStatement(ident);

            return new ElseIfStatement(stmp, con);
        }

        private Statement ReadeElseStatement(string ident)
        {
            if (tokens[index++].type != EnumTokens.ELSE) throw new Exception("Expected ELSE");

            if (tokens[index++].type != EnumTokens.LEFT_BRACE) throw new Exception("Expected LEFT_BRACE");
            Statement stmp = ReadeFunctionStatement(ident);

            return new ElseStatement(stmp);
        }

        public Statement ReadWhileStatement(string ident)
        {
            if (tokens[index++].type != EnumTokens.WHILE) throw new Exception("Expected WHILE");
            if (tokens[index++].type != EnumTokens.LEFT_PAREN) throw new Exception("Expected LEFT_PAREN");
            Condition con = Condition();
            if (tokens[index++].type != EnumTokens.RIGHT_PAREN) throw new Exception("Expected RIGHT_PAREN");
            if (tokens[index++].type != EnumTokens.LEFT_BRACE) throw new Exception("Expected LEFT_BRACE");

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

                expres = new VariableExpression(act.literal);

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
                if (tokens[index++].type != EnumTokens.LEFT_PAREN) throw new Exception("Expected LEFT_PAREN");
                if (tokens[index++].type != EnumTokens.RIGHT_PAREN) throw new Exception("Expected RIGHT_PAREN");
                return new VariableRandom();
            }

            else throw new Exception("Not suported");
        }

        private Statement STRINGRetyping()
        {
            index++;
            if (tokens[index++].type != EnumTokens.RIGHT_PAREN) throw new Exception("Expected RIGHT_PAREN");
            return new ToStringStatemant(tokens[index]);
        }

        private Statement FLOATRetyping()
        {
            index++;
            if (tokens[index++].type != EnumTokens.RIGHT_PAREN) throw new Exception("Expected RIGHT_PAREN");
            return new ToFloatStatemamant(tokens[index]);
        }

        private Statement INTRetyping()
        {
            index++;
            if (tokens[index++].type != EnumTokens.RIGHT_PAREN) throw new Exception("Expected RIGHT_PAREN");
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
