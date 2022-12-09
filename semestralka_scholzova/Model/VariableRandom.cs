using System;

namespace semestralka_scholzova.Model
{
    internal class VariableRandom : Expression
    {
        public VariableRandom()
        {
        }

        public override object Evaluate(ExecutionContext ex)
        {


            Random rnd = new Random();
            int num = rnd.Next(0, 11);
            return num;
        }
    }
}