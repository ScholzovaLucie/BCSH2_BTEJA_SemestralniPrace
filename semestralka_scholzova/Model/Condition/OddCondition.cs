using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Model
{
    public class OddCondition : Condition
    {
        public Expression right;

        public OddCondition(Expression right)
        {
            this.right = right;
        }

        public override bool Evaluate(ExecutionContext ex)
        {
            object value = right.Evaluate(ex);

            if (value.GetType() == typeof(string)) { UserException exception = new UserException(ex.program); }

            if ((int)value % 2 == 0) return false;

            else return true;
        }
    }
}
