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
                        if(val==null)
                        {
                            ex.program.CustomConsole += "-> Chyba přiřazení";
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
                            if (var.type.Equals("int") && val.GetType() == typeof(int))
                            {
                                var.value = val;
                            }
                            else if (var.type.Equals("string") && !int.TryParse((string)val, out value))
                            {
                                var.value = val;
                            }
                            else if (var.type.Equals("float") && val.GetType() == typeof(float))
                            {
                                var.value = val;
                            }
                            else if (var.type.Equals("boolean") && (val.Equals("true") || val.Equals("false")))
                            {
                                var.value = val;
                            }
                        }
                    }
                }
            }

        }
    }
}
