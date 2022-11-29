using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Interpret
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
            Boolean isInLocal = false;
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
                        if (var.type == "INT" && val.GetType() == typeof(int))
                        {
                            var.value = val;
                        }
                        if (var.type == "STRING" && !int.TryParse((string)val, out value))
                        {
                            var.value = val;
                        }
                    }

                }
            }
            else
            {
                foreach (Let var in ex.global.vars)
                {
                    if (var.ident.Equals(token.lexeme))
                    {
                        int value;
                        val = expression.Evaluate(ex);
                        if (var.type == "INT" && val.GetType() == typeof(int))
                        {
                            var.value = val;
                        }
                        if (var.type == "STRING" && !int.TryParse((string)val, out value))
                        {
                            var.value = val;
                        }
                    }
                }
            }

        }
    }
}
