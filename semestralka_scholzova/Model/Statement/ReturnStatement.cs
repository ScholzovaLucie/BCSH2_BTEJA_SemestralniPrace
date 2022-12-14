using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace semestralka_scholzova.Model
{
    public class ReturnStatement : Statement
    {
        private Expression expression;
        private string ident { get; set; }

        public ReturnStatement(Expression ex, string ident)
        {
            expression = ex;

            this.ident = ident;
        }

        public override void Execute(ExecutionContext ex)
        {
            foreach (Function func in ex.pc.Functions)
            {
                if (func.iden.Equals(ident))
                {
                    if(func.returnvalue==null)
                    func.returnvalue = expression.Evaluate(ex);
                }
                if (func.type.ToString().Equals("float"))
                {
                    object type = func.returnvalue.GetType().Name;
                    if (!type.Equals("Single"))
                    {
                        UserException exp = new UserException(ex.program, "Špatný návratový typ");
                        
                    }
                }
                else if (!func.returnvalue.GetType().Name.ToLower().Contains(func.type.ToString()))
                {
                    UserException exp = new UserException(ex.program, "Špatný návratový typ");
                }
            }

        }
    }
}
