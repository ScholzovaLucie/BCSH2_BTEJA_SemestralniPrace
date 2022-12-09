using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Model
{
    public abstract class Condition
    {
        public abstract bool Evaluate(ExecutionContext ex);
    }
}
