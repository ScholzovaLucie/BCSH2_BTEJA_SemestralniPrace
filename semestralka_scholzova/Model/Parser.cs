
using semestralka_scholzova;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace semestralka_scholzova.Model
{
    public class Parser
    {
        public List<Token> tokens = new List<Token>();
        public int index = 0;
        Block block = new Block();
        UserException exception;
        public bool chyba = false;
        Program pr;

        public Parser(List<Token> tokens, Program pr)
        {
            this.tokens = tokens;
            this.pr = pr;
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
                else if (tokens[index].type == EnumTokens.WRITEFILE) block.Statements.Add(ReadWriteFileStatement());

                else goto nastalachyba;
            }
            nastalachyba:
            return block;
        }

        private Statement ReadWriteFileStatement()
        {
            Token tok = null;
            index++;
            string fileName;
            Expression text;
            if (tokens[index++].type != EnumTokens.LEFT_PAREN) { exception = new UserException(pr, "Expected LEFT_PAREN"); chyba = true; return null; }
            if(tokens[index].type != EnumTokens.STRING) { exception = new UserException(pr, "Expected STRING"); chyba = true; return null; }
            fileName = tokens[index++].lexeme;
            if (tokens[index++].type != EnumTokens.COMMA) { exception = new UserException(pr, "Expected COMMA"); chyba = true; return null; }
            text = Expression();
            if (tokens[index++].type != EnumTokens.RIGHT_PAREN) { exception = new UserException(pr, "Expected RIGHT_PAREN"); chyba = true; return null; }
            if (tokens[index++].type != EnumTokens.SEMICOLON) { exception = new UserException(pr, "Expected SEMICOLON"); chyba = true; return null; }

            return new WriteFileStatemant(fileName, text);;
        }

        private Statement ReadReadFileStatemant(Token ident)
        {
            Token tok = null;
            index++;
            string fileName;
            Expression text;
            if (tokens[index++].type != EnumTokens.LEFT_PAREN) { exception = new UserException(pr, "Expected LEFT_PAREN"); chyba = true; return null; }
            if (tokens[index].type != EnumTokens.STRING) { exception = new UserException(pr, "Expected STRING"); chyba = true; return null; }
            fileName = tokens[index++].lexeme;
            if (tokens[index++].type != EnumTokens.RIGHT_PAREN) { exception = new UserException(pr, "Expected RIGHT_PAREN"); chyba = true; return null; }
            if (tokens[index++].type != EnumTokens.SEMICOLON) { exception = new UserException(pr, "Expected SEMICOLON"); chyba = true; return null; }

            return new ReadeFileStatemant(fileName, ident); ;
        }

        private (Let, Statement) ReadVar()
        {
            Let newVar;
            Token type;

            if (tokens[index++].type != EnumTokens.LET) { exception = new UserException(pr, "Expected LET"); chyba = true; return (null, null); }
            if (tokens[index].type != EnumTokens.VARIABLE) { exception = new UserException(pr, "Expected VARIABLE"); chyba = true; return (null, null); }
            Token ident = tokens[index++];

            if (tokens[index++].type != EnumTokens.COLON) { exception = new UserException(pr, "Expected COLON"); chyba = true; return (null, null); }

            if (tokens[index].type == EnumTokens.INT || 
                tokens[index].type == EnumTokens.FLOAT || 
                tokens[index].type == EnumTokens.BOOLEAN || 
                tokens[index].type == EnumTokens.STRING) {  type = tokens[index++]; }
            else { exception = new UserException(pr, "Expected TYPE"); return (null, null); }


            if (tokens[index++].type != EnumTokens.EQUAL) {
                newVar = new Let(ident.literal, type.literal);
                return (newVar, null);
            }

            if (tokens[index].type == EnumTokens.READ)
            {
                newVar = new Let(ident.literal, type.literal);
                return (newVar, ReadReadStatement(ident));
            }

            if(tokens[index].type == EnumTokens.READFILE)
            {
                newVar = new Let(ident.literal, type.literal);
                return (newVar, ReadReadFileStatemant(ident));
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

                if (tokens[index++].type != EnumTokens.SEMICOLON) { exception = new UserException(pr, "Expected SEMICOLON"); chyba = true; return (null, null); }
                newVar = new Let(ident.literal, type.literal);
                return (newVar, st);

            }
            Expression ex = ConExpression();
            SetStatement setstmp = new SetStatement(ident, type.literal, ex);

            if (tokens[index++].type != EnumTokens.SEMICOLON) { exception = new UserException(pr, "Expected SEMICOLON"); chyba = true; return (null, null); }

            newVar = new Let(ident.literal, type.literal);
            return (newVar, setstmp);

        }

        private Function ReadeFunction()
        {
            UserException exception;
            Token FunctionType;
            List<Let> para = new List<Let>();
            Token type;

            if (tokens[index++].type != EnumTokens.FUNCTION) { exception = new UserException(pr, "Expected FUNCTION"); chyba = true; return null; }
            if (tokens[index].type != EnumTokens.VARIABLE) { exception = new UserException(pr, "Expected VARIABLE"); chyba = true; return null; }
            Token ident = tokens[index++];
            if (tokens[index++].type != EnumTokens.LEFT_PAREN) { exception = new UserException(pr, "Expected LEFT_PAREN"); chyba = true; return null; }
            if (tokens[index].type == EnumTokens.VARIABLE)
            {
                Token par = tokens[index++];
                if (tokens[index++].type != EnumTokens.COLON) { exception = new UserException(pr, "Expected COLON"); chyba = true; return null; }
                if (tokens[index].type == EnumTokens.INT ||
                    tokens[index].type == EnumTokens.FLOAT ||
                    tokens[index].type == EnumTokens.BOOLEAN ||
                    tokens[index].type == EnumTokens.VOID ||
                    tokens[index].type == EnumTokens.STRING) { type = tokens[index++]; }
                else { exception = new UserException(pr,"Expected Type"); chyba = true; return null; }
                

                para.Add(new Let(par.lexeme, type.literal, null));

                while (tokens[index].type != EnumTokens.RIGHT_PAREN)
                {
                    if (tokens[index++].type != EnumTokens.COMMA) { exception = new UserException(pr, "Expected COMMA"); chyba = true; return null; }
                    if (tokens[index].type != EnumTokens.VARIABLE) { exception = new UserException(pr, "Expected VARIABLE"); chyba = true; return null; }
                    Token par2 = tokens[index++];
                    if (tokens[index++].type != EnumTokens.COLON) { exception = new UserException(pr, "Expected COLON"); chyba = true; return null; }
                    if (tokens[index].type == EnumTokens.INT ||
                        tokens[index].type == EnumTokens.FLOAT ||
                        tokens[index].type == EnumTokens.BOOLEAN ||
                        tokens[index].type == EnumTokens.STRING) { type = tokens[index++];}
                    else { exception = new UserException(pr,"Expected Type"); chyba = true; return null; }

                    para.Add(new Let(par2.lexeme, type.literal, null));
                }
                if (tokens[index].type != EnumTokens.RIGHT_PAREN) { exception = new UserException(pr, "Expected RIGHT_PAREN"); chyba = true; return null; }
            }
            if (tokens[index++].type != EnumTokens.RIGHT_PAREN) { exception = new UserException(pr, "Expected RIGHT_PAREN"); chyba = true; return null; }
            if (tokens[index++].type != EnumTokens.COLON) { exception = new UserException(pr,"Expected COLON"); chyba = true; return null; }
            if (tokens[index].type == EnumTokens.INT ||
                tokens[index].type == EnumTokens.FLOAT ||
                tokens[index].type == EnumTokens.BOOLEAN ||
                tokens[index].type == EnumTokens.STRING ||
                tokens[index].type == EnumTokens.VOID) { FunctionType = tokens[index++]; }
            else { exception = new UserException(pr, "Expected Function Type"); chyba = true; return null; }
            
            if (tokens[index++].type != EnumTokens.LEFT_BRACE) { exception = new UserException(pr, "Expected LEFT_BRACE"); chyba = true; return null; }
            FunctionStatement st = ReadeFunctionStatement(ident.lexeme);

            Function fun = new Function(ident.lexeme, FunctionType.lexeme);
            fun.parameters = para;
            fun.Statements = st.stmp;
            fun.Functions = st.functions;
            fun.vars = st.vars;
            if (FunctionType.type == EnumTokens.INT || FunctionType.type == EnumTokens.FLOAT || FunctionType.type == EnumTokens.BOOLEAN || FunctionType.type == EnumTokens.STRING)
            {
                if (!st.GetList().OfType<ReturnStatement>().Any())
                {
                    exception = new UserException(pr, "Missing return"); chyba = true; return null;
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
                switch (tokens[index].type)
                {
                    case EnumTokens.LET:
                        Statement st;
                        Let let;
                        (let, st) = ReadVar();
                        variables.Add(let);
                        stmp.Add(st);
                        break;

                    case EnumTokens.FUNCTION:
                        functions.Add(ReadeFunction());
                        break;
                    case EnumTokens.WHILE:
                        stmp.Add(ReadWhileStatement(ident));
                        break;
                    case EnumTokens.IF:
                        stmp.Add(ReadIfStatement(ident));
                        break;
                    case EnumTokens.WRITE:
                        stmp.Add(ReadWriteStatement());
                        break;
                    case EnumTokens.WRITEFILE:
                        stmp.Add(ReadWriteFileStatement());
                        break;
                    case EnumTokens.VARIABLE:
                        stmp.Add(setVariable());
                        break;
                    case EnumTokens.RETURN:
                        stmp.Add(ReadeReturnStatement(ident));
                        break;
                    case EnumTokens.CONTINUE:
                        stmp.Add(new ContinueStatemant());
                        index++;
                        if (tokens[index++].type != EnumTokens.SEMICOLON){ exception = new UserException(pr, "Expected SEMICOLON"); chyba = true;}
                        break;
                    case EnumTokens.BREAK:
                        stmp.Add(new BreakeStatemant());
                        index++;
                        if (tokens[index++].type != EnumTokens.SEMICOLON){ exception = new UserException(pr, "Expected SEMICOLON"); chyba = true;}
                        break;

                }
            }

            if (tokens[index++].type != EnumTokens.RIGHT_BRACE) { exception = new UserException(pr,"Expected RIGHT_BRACE"); chyba = true; return null; }

            return new FunctionStatement(stmp, variables, functions);
        }

        public Statement ReadeReturnStatement(string ident)
        {
  
            Token token = tokens[index++];
            if (nasledujici() == "(")
            {
                ReturnStatement st = new ReturnStatement(new CallExpression(tokens[index++]), ident);
                if (tokens[index++].type != EnumTokens.SEMICOLON) { exception = new UserException(pr, "Expected SEMICOLON"); chyba = true; return null; }
                return st;
            }
            else if (tokens[index].lexeme == ";")
            {
                EmptyReturnStatemant con =  new EmptyReturnStatemant();
                if (tokens[index++].type != EnumTokens.SEMICOLON)
                {
                    exception = new UserException(pr,"Expected SEMICOLON");
                    chyba = true;

                }
                return con;
            }
            else
            {
                ReturnStatement st = new ReturnStatement(Expression(), ident);
                if (tokens[index++].type != EnumTokens.SEMICOLON) { exception = new UserException(pr, "Expected SEMICOLON"); chyba = true; return null; }
                return st;
            }

        }


        public Statement ReadReadStatement(Token ident)
        {
            UserException exception;
            if (tokens[index].type == EnumTokens.READ)
            {
                ReadeStatement st = new ReadeStatement(ident);
                index++;
                if (tokens[index++].type != EnumTokens.LEFT_PAREN) { exception = new UserException(pr, "Expected LEFT_PAREN"); chyba = true; return null; }
                if (tokens[index++].type != EnumTokens.RIGHT_PAREN) {exception = new UserException(pr, "Expected RIGHT_PAREN"); chyba = true; return null; }
                if (tokens[index++].type != EnumTokens.SEMICOLON) { exception = new UserException(pr, "Expected SEMICOLON"); chyba = true; return null; }
                return st;
            }
            else { exception = new UserException(pr,"Expected READE"); return null; }
        }

        public Statement ReadWriteStatement()
        {
            UserException exception;
            if (tokens[index++].type == EnumTokens.WRITE)
            {
                if (tokens[index++].type != EnumTokens.LEFT_PAREN) { exception = new UserException(pr,"Expected LEFT_PAREN"); chyba = true; return null;}
                    Statement st = new WriteStatement(Expression());
                    if (tokens[index++].type != EnumTokens.RIGHT_PAREN) { exception = new UserException(pr,"Expected RIGHT_PAREN"); chyba = true; return null; }
                    if (tokens[index++].type != EnumTokens.SEMICOLON) { exception = new UserException(pr,"Expected SEMICOLON"); chyba = true; return null; }
                    return st;
            }
            else { exception = new UserException(pr,"Expected WRITE"); chyba = true; return null; }


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
                        Statement st = new SetStatement(idnet, idnet.lexeme, ConExpression());

                        if (tokens[index++].type != EnumTokens.SEMICOLON) { exception = new UserException(pr, "Expected SEMICOLON"); chyba = true; return null; }
                        return st;
                    }
                    if (tokens[index].type == EnumTokens.RANDOM)
                    {
                        Statement st = new SetStatement(idnet, idnet.lexeme, ConExpression());

                        if (tokens[index++].type != EnumTokens.SEMICOLON) { exception = new UserException(pr, "Expected SEMICOLON"); chyba = true; return null; }
                        return st;
                    }
                    if (tokens[index].type == EnumTokens.READ)
                    {

                    }
                    Token vare = tokens[index++];
                    if (tokens[index].type == EnumTokens.SEMICOLON)
                    {
                        index--;
                        Statement st = new SetStatement(idnet, idnet.type, ConExpression());
                        if (tokens[index++].type != EnumTokens.SEMICOLON) { exception = new UserException(pr, "Expected SEMICOLON"); chyba = true; return null; }

                            return st;
                    }

                    if (tokens[index].type == EnumTokens.LEFT_PAREN)
                    {
                        index++;
                        if (tokens[index++].type != EnumTokens.RIGHT_PAREN) { exception = new UserException(pr, "Expected RIGHT_PAREN"); chyba = true; return null; }
                        if (tokens[index++].type != EnumTokens.SEMICOLON) { exception = new UserException(pr, "Expected SEMICOLON"); chyba = true; return null; }
                        return new SetStatement(idnet, idnet.type, new CallExpression(vare));
                    }

                    if (tokens[index].type == EnumTokens.BOOLEAN)
                    {
                        Statement st = new SetStatement(vare, EnumTokens.BOOLEAN, ConExpression());
                        if (tokens[index++].type != EnumTokens.SEMICOLON) { exception = new UserException(pr, "Expected SEMICOLON"); chyba = true; return null; }
                            return st;
                    }
                }
                else
                {
                    index = index - 2; ;
                    Token vare = tokens[index++];
                    List<object> list = new List<object>();
                    if (tokens[index++].type != EnumTokens.LEFT_PAREN) { exception = new UserException(pr, "Expected LEFT_PAREN"); chyba = true; return null; }
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
                    if (tokens[index++].type != EnumTokens.RIGHT_PAREN) { exception = new UserException(pr, "Expected RIGHT_PAREN"); chyba = true; return null; }
                    if (tokens[index++].type != EnumTokens.SEMICOLON) { exception = new UserException(pr, "Expected SEMICOLON"); chyba = true; return null; }
                        CallStatement call = new CallStatement(vare);
                    call.parameters = list;
                    return call;


                }

            }

            exception = new UserException(pr);
            chyba = true;
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
            if (tokens[index++].type != EnumTokens.IF) { exception = new UserException(pr,"Expected IF"); chyba = true; return null; }
            if (tokens[index++].type != EnumTokens.LEFT_PAREN) { exception = new UserException(pr,"Expected LEFT_PAREN"); chyba = true; return null; }

            Condition con = Condition();
            if (tokens[index++].type != EnumTokens.RIGHT_PAREN) { exception = new UserException(pr, "Expected RIGHT_PAREN"); chyba = true; return null; }
            if (tokens[index++].type != EnumTokens.LEFT_BRACE) { exception = new UserException(pr,"Expected LEFT_BRACE"); chyba = true; return null; }
            List<Statement> stmps;
            List<ElseIfStatement> elseifstmp;
            ElseStatement elsestmp;
            List<Let> variables = new List<Let>();

            (stmps, elseifstmp, elsestmp, variables) = ReadIfFunctionStatement(ident);

            return new IfStatement(stmps, con, elseifstmp, elsestmp, variables);
        }

        private (List<Statement>, List<ElseIfStatement>, ElseStatement, List<Let>) ReadIfFunctionStatement(string ident)
        {
            List<Statement> stmp = new List<Statement>();
            List<ElseIfStatement> elseifstmp = new List<ElseIfStatement>();
            ElseStatement elsestmp = null;
            List<Let> variables = new List<Let>();
            List<Function> functions = new List<Function>();

            while (tokens[index].type != EnumTokens.RIGHT_BRACE)
            {
                switch (tokens[index].type)
                {
                    case EnumTokens.LET:
                        Statement st;
                        Let let;
                        (let, st) = ReadVar();
                        variables.Add(let);
                        stmp.Add(st);
                        break;

                    case EnumTokens.FUNCTION:
                        functions.Add(ReadeFunction());
                        break;
                    case EnumTokens.WHILE:
                        stmp.Add(ReadWhileStatement(ident));
                        break;
                    case EnumTokens.IF:
                        stmp.Add(ReadIfStatement(ident));
                        break;
                    case EnumTokens.WRITE:
                        stmp.Add(ReadWriteStatement());
                        break;
                    case EnumTokens.WRITEFILE:
                        stmp.Add(ReadWriteFileStatement());
                        break;
                    case EnumTokens.VARIABLE:
                        stmp.Add(setVariable());
                        break;
                    case EnumTokens.RETURN:
                        stmp.Add(ReadeReturnStatement(ident));
                        break;
                    case EnumTokens.CONTINUE:
                        stmp.Add(new ContinueStatemant());
                        index++;
                        if (tokens[index++].type != EnumTokens.SEMICOLON) { exception = new UserException(pr, "Expected SEMICOLON"); chyba = true; }
                        break;
                    case EnumTokens.BREAK:
                        stmp.Add(new BreakeStatemant());
                        index++;
                        if (tokens[index++].type != EnumTokens.SEMICOLON) { exception = new UserException(pr, "Expected SEMICOLON"); chyba = true; }
                        break;

                }

            }

            if (tokens[index++].type != EnumTokens.RIGHT_BRACE) { exception = new UserException(pr,"Expected RIGHT_BRACE"); chyba = true; return (null, null, null, null); }

                if (tokens[index].type == EnumTokens.ELSEIF)
            {
                while (tokens[index].type != EnumTokens.ELSE) elseifstmp.Add(ReadeElseIfStatement(ident));
            }

            if (tokens[index].type == EnumTokens.ELSE) elsestmp = ReadeElseStatement(ident);

            return (stmp, elseifstmp, elsestmp, variables);
        }

        private ElseIfStatement ReadeElseIfStatement(string ident)
        {
            if (tokens[index++].type != EnumTokens.ELSEIF) { exception = new UserException(pr, "Expected ELSEIF"); chyba = true; return null; }
            if (tokens[index++].type != EnumTokens.LEFT_PAREN) { exception = new UserException(pr, "Expected LEFT_PAREN"); chyba = true; return null; }
            Condition con = Condition();
            if (tokens[index++].type != EnumTokens.RIGHT_PAREN) { exception = new UserException(pr, "Expected RIGHT_PAREN"); chyba = true; return null; }
            if (tokens[index++].type != EnumTokens.LEFT_BRACE) { exception = new UserException(pr, "Expected LEFT_BRACE"); chyba = true; return null; }

            Statement stmp = ReadeFunctionStatement(ident);

            return new ElseIfStatement(stmp, con);
        }

        private ElseStatement ReadeElseStatement(string ident)
        {
            if (tokens[index++].type != EnumTokens.ELSE) { exception = new UserException(pr,"Expected ELSE"); return null; }

            if (tokens[index++].type != EnumTokens.LEFT_BRACE) { exception = new UserException(pr,"Expected LEFT_BRACE"); return null; }
            Statement stmp = ReadeFunctionStatement(ident);

            return new ElseStatement(stmp);
        }

        public Statement ReadWhileStatement(string ident)
        {
            if (tokens[index++].type != EnumTokens.WHILE) { exception = new UserException(pr,"Expected WHILE"); chyba = true; return null; }
            if (tokens[index++].type != EnumTokens.LEFT_PAREN) { exception = new UserException(pr,"Expected LEFT_PAREN"); chyba = true; return null; }
            Condition con = Condition();
            if (tokens[index++].type != EnumTokens.RIGHT_PAREN) { exception = new UserException(pr); chyba = true; return null; }
                if (tokens[index++].type != EnumTokens.LEFT_BRACE) { exception = new UserException(pr,"Expected LEFT_BRACE"); chyba = true; return null; }

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

        private Expression ConExpression()
        {
            Expression expr = Expression();
           
            EnumTokens[] types = { EnumTokens.EQUAL_EQUAL, EnumTokens.GREATER, EnumTokens.GREATER_EQUAL, EnumTokens.LESS, EnumTokens.LESS_EQUAL, EnumTokens.ODD, EnumTokens.HASHTAG, EnumTokens.BANG_EQUAL };
            while (ContainsAktualToken(types))
            {
                Token operatorVar = tokens[index++];
                Expression right = Expression();
                expr = new ConditionExpression(expr, operatorVar, right);
            }

            return expr;
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
            EnumTokens[] types = { EnumTokens.SLASH, EnumTokens.STAR , EnumTokens.ODD};
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
                    if (tokens[index++].type != EnumTokens.LEFT_PAREN) { exception = new UserException(pr, "Expected LEFT_PAREN"); chyba = true; return null; }
                    if (tokens[index++].type != EnumTokens.RIGHT_PAREN) { exception = new UserException(pr, "Expected RIGHT_PAREN"); chyba = true; return null; }
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
                if (tokens[index++].type != EnumTokens.LEFT_PAREN) { exception = new UserException(pr, "Expected LEFT_PAREN"); chyba = true; return null; }
                if (tokens[index++].type != EnumTokens.RIGHT_PAREN) { exception = new UserException(pr, "Expected RIGHT_PAREN"); chyba = true; return null; }
                return new VariableRandom();
            }

    else { exception = new UserException(pr); chyba = true; return  null; }
}

        private Statement STRINGRetyping()
        {
            index++;
            if (tokens[index++].type != EnumTokens.RIGHT_PAREN) { exception = new UserException(pr, "Expected RIGHT_PAREN"); chyba = true; return null; }
    return new ToStringStatemant(tokens[index]);
        }

        private Statement FLOATRetyping()
        {
            index++;
            if (tokens[index++].type != EnumTokens.RIGHT_PAREN) { exception = new UserException(pr,"Expected RIGHT_PAREN"); chyba = true;  return null; }
            return new ToFloatStatemamant(tokens[index]);
        }

        private Statement INTRetyping()
        {
            index++;
            if (tokens[index++].type != EnumTokens.RIGHT_PAREN) { exception = new UserException(pr,"Expected RIGHT_PAREN"); chyba = true;  return null; }
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
