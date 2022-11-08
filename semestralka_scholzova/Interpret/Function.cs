using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Interpret
{
    public class Function
    {
        public Token iden;

        public object type;
        public List<Let> parameters;

        public Statement stmp;

        public Function(Token ident, object type)
        {
            this.iden = ident;
            this.type = type;
        }


    }
}
