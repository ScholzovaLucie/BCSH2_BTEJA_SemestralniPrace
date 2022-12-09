using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Model
{
    public class Let
    {
        public object ident { get; set; }

        public object value { get; set; }

        public object type { get; set; }

        public Let(object ident, object type, object value)
        {
            this.ident = ident;
            this.type = type;
            this.value = value;
        }

        public Let(object ident, object type)
        {
            this.ident = ident;
            this.type = type;
        }
    }
}
