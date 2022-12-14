using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace semestralka_scholzova.Model
{
    public class SetStatement : Statement
    {
        private Token token;
        private object type;
        private Expression expression;

        public SetStatement(Token token, object type, Expression expression)
        {
            this.token = token;
            this.expression = expression;
            this.type = type;
        }

        public override void Execute(ExecutionContext ex)
        {
            bool isInLocal = false;
            object val = null;
            if (ex.vars != null)
            {
                foreach (Let var in ex.vars)
                {
                    if (var.ident.Equals(token.lexeme)) isInLocal = true;
                }
            }

            if (isInLocal == true)
            {
                foreach (Let var in ex.vars)
                {
                    if (var.ident.Equals(token.lexeme))
                    {
                        int value;
                        val = expression.Evaluate(ex);
                        var.value = vyhodnoceni(val, var, ex);
                       
                    }

                }
            }
            else
            {
                foreach (Function fc in ex.pc.Functions)
                {
                    foreach (Let var in fc.vars)
                    {
                        if (var.ident.Equals(token.lexeme))
                        {
                            int value;
                            val = expression.Evaluate(ex);
                            var.value = vyhodnoceni(val, var, ex);
                        }
                    }
                }
                foreach (Statement st in ex.statements)
                {
                    foreach (Let var in st.vars)
                    {
                        if (var.ident.Equals(token.lexeme))
                        {
                            int value;
                            val = expression.Evaluate(ex);
                            var.value = vyhodnoceni(val, var, ex);
                        }
                    }
                    if(st is IfStatement)
                    {
                        IfStatement ifs = (IfStatement)st;
                        foreach (Let var in ifs.elseStatement.statement.vars)
                        {
                            if (var.ident.Equals(token.lexeme))
                            {
                                int value;
                                val = expression.Evaluate(ex);
                                var.value = vyhodnoceni(val, var, ex);
                            }
                        }

                        foreach (ElseIfStatement eist in ifs.elseifstatement)
                        {
                            foreach (Let var in eist.vars)
                            {
                                if (var.ident.Equals(token.lexeme))
                                {
                                    int value;
                                    val = expression.Evaluate(ex);
                                    var.value = vyhodnoceni(val, var, ex);
                                }
                            }
                        }
                    }

                    if(st is WhileStatement)
                    {
                        WhileStatement ifs = (WhileStatement)st;
                        foreach (Let var in ifs.statement.vars)
                        {
                            if (var.ident.Equals(token.lexeme))
                            {
                                int value;
                                val = expression.Evaluate(ex);
                                var.value = vyhodnoceni(val, var, ex);
                            }
                        }
                    }

                }
            }

        }
        private object vyhodnoceni(object val, Let var, ExecutionContext ex)
        {
            if (val == null)
            {
                ex.program.CustomConsole += "-> Chyba přiřazení\n";
                UserException userEx = new UserException(ex.program);
            }
            else
            {
                if (var.type.Equals("int") && val.GetType() == typeof(int))
                {
                    var.value = val;
                }
                else if (var.type.Equals("string") && val.GetType() == typeof(string))
                {
                    var.value = val;
                }
                else if (var.type.Equals("float") && val.GetType() == typeof(Single))
                {
                    var.value = val;
                }
                else if (var.type.Equals("boolean") && (val.Equals("true") || val.Equals("false")))
                {
                    var.value = val;
                }
                else if (var.type.Equals("boolean") && ((bool)val == true || (bool)val == false))
                {
                    var.value = val;
                }
                else
                {
                    foreach (Function func in ex.pc.Functions)
                    {
                        if (func.iden.Equals(val))
                        {
                            if (func.returnvalue == null)
                            {
                                foreach (Statement st in func.Statements)
                                {
                                    st.Execute(ex);
                                }
                            }
                            foreach (Statement st in func.Statements)
                            {
                                if (st.GetType() == typeof(ReturnStatement) || st.GetType() == typeof(ContinueStatemant) || st.GetType() == typeof(BreakeStatemant))

                                {
                                    var.value = func.returnvalue;
                                }
                            }

                        }
                    }
                }

            }

            return var.value;
        }
    }
}
