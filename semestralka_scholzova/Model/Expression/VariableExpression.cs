using System;

namespace semestralka_scholzova.Model
{
    public class VariableExpression : Expression
    {
        public object literal;

        public VariableExpression(object literal)
        {
            this.literal = literal;
        }

        public override object Evaluate(ExecutionContext ex)
        {

            bool isInLocal = false;
            if (ex.vars != null)
            {
                foreach (Let var in ex.vars)
                {
                    if (var.ident.Equals(literal)) isInLocal = true;

                }
            }

            if (isInLocal == true)
            {
                foreach (Let var in ex.vars)
                {
                    if (var.ident.Equals(literal)) return var.value;
                }

            }
            else
            {
                foreach (Function func in ex.pc.Functions)
                {
                    foreach (Let var in func.vars)
                        if (var.ident.Equals(literal)) return var.value;


                }

                foreach (Statement st in ex.statements)
                {
                    foreach (Let var in st.vars)
                    {
                        if (var.ident.Equals(literal))
                        {
                            return var.value;
                        }
                    }
                    if (st is IfStatement)
                    {
                        IfStatement ifs = (IfStatement)st;
                        foreach (Let var in ifs.elseStatement.statement.vars)
                        {
                            if (var.ident.Equals(literal))
                            {
                                return var.value;
                            }
                        }
                    
                        foreach (ElseIfStatement eist in ifs.elseifstatement)
                        {
                            foreach (Let var in eist.vars)
                            {
                                if (var.ident.Equals(literal))
                                {
                                    return var.value;
                                }
                            }
                        }
                    }
                       

                    if (st is WhileStatement)
                    {
                        WhileStatement ifs = (WhileStatement)st;
                        foreach (Let var in ifs.statement.vars)
                        {
                            if (var.ident.Equals(literal))
                            {
                                return var.value;
                            }
                        }
                    }

                }
                

            }
                    return literal;
        }
    }
}
