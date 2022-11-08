using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Interpret
{
    public class SetStatement : Statement
    {
        private object token;
        private object type;
        private object expression;

        public SetStatement(object token, object type, object expression)
        {
            this.token = token;
            this.expression = expression;
            this.type = type;
        }

        public override void Execute(ExecutionContext ex)
        {
           
            
        }
    }
}
