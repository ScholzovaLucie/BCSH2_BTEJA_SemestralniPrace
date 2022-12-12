using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Model
{
    internal class ReadeStatement : Statement
    {
        private Token token;

        public ReadeStatement(Token token)
        {
            this.token = token;
        }

        public override void Execute(ExecutionContext ex)
        {
            foreach (Let var in ex.vars)
            {
                if (var.ident.Equals(token.lexeme))
                {
                    ReadeFromConsole reade = new ReadeFromConsole();
                    string value = reade.reade();
                    var.value = value;

                }
            }
        }


    }
}
