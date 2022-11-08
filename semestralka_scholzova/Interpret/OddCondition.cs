using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Interpret
{
    public class OddCondition : Condition
    {
        public Expression right;

        public OddCondition(Expression right)
        {
            this.right = right;
        }

        public override Boolean Evaluate(ExecutionContext ex)
        {
            if (right.Evaluate(ex) % 2 == 0) return false;

            else return true;
        }
    }
}
