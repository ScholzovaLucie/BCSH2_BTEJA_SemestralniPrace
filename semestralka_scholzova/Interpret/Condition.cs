﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Interpret
{
    public abstract class Condition
    {
        public abstract Boolean Evaluate(ExecutionContext ex);
    }
}
