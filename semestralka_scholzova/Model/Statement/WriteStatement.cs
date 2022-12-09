﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            object value = expression.Evaluate(ex);
            ex.Console += value.ToString();
            ex.Console += "\n";
        }
    }
}