using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Model
{
    internal class ToFloatStatemamant : Statement
    {
        private Token token;

        public ToFloatStatemamant(Token token)
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
                        if (var.type == "STRING")
                        {
                            float value;
                            float.TryParse((string)var.value, out value);
                            var.value = value;
                            var.type = "FLOAT";
                        }
                        else if (var.type == "INT")
                        {
                            var.value = (float)var.value;
                            var.type = "FLOAT";
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
                            if (var.type == "STRING")
                            {
                                float value;
                                float.TryParse((string)var.value, out value);
                                var.value = value;
                                var.type = "FLOAT";
                            }
                            else if (var.type == "INT")
                            {
                                var.value = (float)var.value;
                                var.type = "FLOAT";
                            }
                        }
                    }
                }

            }
        }
    }
}
