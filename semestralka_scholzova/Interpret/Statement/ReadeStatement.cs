using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Interpret
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
            foreach(Let var in ex.vars)
            {
                if (var.ident.Equals(token.lexeme))
                {
                    Console.WriteLine("Zadej hodnotu:");
                    var.value = double.Parse(Console.ReadLine());
                }
            }
        }


    }
}
