using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace semestralka_scholzova.Model
{
    internal class WriteStatement : Statement
    {
        private Expression expression;

        public WriteStatement(Expression expression)
        {
            this.expression = expression;
        }

        public override void Execute(ExecutionContext ex)
        {
            try
            {
                object value = expression.Evaluate(ex);
                if (value != null)
                {
                    ex.program.CustomConsole += value.ToString();
                    ex.program.CustomConsole += "\n";
                }
                else{
                    ex.program.CustomConsole += "-> chyba ve výpisu";
                }
            }catch(Exception exep)
            {
                UserException userExp = new UserException(ex.program,"Cyhba ve vípisu");
               
            }
           

        }
    }
}
