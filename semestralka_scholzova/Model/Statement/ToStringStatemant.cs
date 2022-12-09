using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Model
{
    internal class ToStringStatemant : Statement
    {
        private Token token;

        public ToStringStatemant(Token token)
        {
            this.token = token;
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
                        if (var.type == "INT")
                        {
                            var.value = var.value.ToString();
                            var.type = "STRING";
                        }
                        else if (var.type == "FLOAT")
                        {
                            var.value = var.value.ToString();
                            var.type = "STRING";
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
                            if (var.type == "INT")
                            {
                                var.value = var.value.ToString();
                                var.type = "STRING";
                            }
                            else if (var.type == "FLOAT")
                            {
                                var.value = var.value.ToString();
                                var.type = "STRING";
                            }
                        }
                    }
                }

            }
        }
    }
}