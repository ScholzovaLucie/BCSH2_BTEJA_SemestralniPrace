using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Model
{
    public abstract class Statement
    {
        public bool breakDone = false;
        public bool continuDone = false;
        public bool returnDone = false;
        public List<Let> vars = new List<Let>();

        public abstract void Execute(ExecutionContext ex);
    }
}
