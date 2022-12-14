using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace semestralka_scholzova.Model
{
    internal class ReadeStatement : Statement
    {
        private Token token;
        private Program pr;

        public ReadeStatement(Token token)
        {
            this.token = token;
        }

        public override void Execute(ExecutionContext ex)
        {
            pr = ex.program;
            try
            {
                foreach (Let var in ex.vars)
                {
                    if (var.ident.Equals(token.lexeme))
                    {
                       var.value = read(ex, var);
                    }
                }

                foreach (Function fc in ex.pc.Functions)
                {
                    foreach (Let var in fc.vars)
                    {
                        if (var.ident.Equals(token.lexeme))
                        {
                            var.value = read(ex, var);
                        }
                    }
                }

                foreach (Statement st in ex.statements)
                {
                    foreach (Let var in st.vars)
                    {
                        if (var.ident.Equals(token.lexeme))
                        {
                            var.value = read(ex, var);
                        }
                    }
                        if (st is IfStatement)
                        {
                            IfStatement ifs = (IfStatement)st;
                            foreach (Let var in ifs.elseStatement.statement.vars)
                            {
                                if (var.ident.Equals(token.lexeme))
                                {
                                    var.value = read(ex, var);
                                }
                            }

                            foreach (ElseIfStatement eist in ifs.elseifstatement)
                            {
                                foreach (Let var in eist.vars)
                                {
                                    if (var.ident.Equals(token.lexeme))
                                    {
                                        var.value = read(ex, var);
                                    }
                                }
                            }
                        }

                    if (st is WhileStatement)
                    {
                        WhileStatement ifs = (WhileStatement)st;
                        foreach (Let var in ifs.statement.vars)
                        {
                            if (var.ident.Equals(token.lexeme))
                            {
                                var.value = read(ex, var);
                            }
                        }
                    }


                }
            }
            catch(Exception e)
            {
                UserException exep = new UserException(pr, "Chyba v načítání z konzole");
                
                
            }
           
        }

        private object read(ExecutionContext ex, Let var)
        {
            ex.program.Editable = false;
            ex.program.Color = "White";

            ex.program.CustomConsole += "-> Zadej hodnotu \n";
            while (true)
            {
                Thread.Sleep(10);
                if (ex.program.ImportConsole != null)
                {
                    break;
                }

            }


            var.value = ex.program.ImportConsole;
            ex.program.CustomConsole += "-> Konec načítání \n";
            ex.program.Editable = true;
            ex.program.Color = "{x:Null}";

            ex.program.ImportConsole = null;
            return var.value;
        }


    }
}
