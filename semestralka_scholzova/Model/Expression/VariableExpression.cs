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


            }
            return literal;
        }
    }
}
