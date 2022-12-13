using semestralka_scholzova.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova
{
    public class UserException
    {
        public UserException(Program pr) {
            pr.CustomConsole += "-> Error: neznámá chyba\n";
            
        }
        public UserException(Program pr, string mess)
        {
            pr.CustomConsole += "-> Error: "+mess+"\n";
        }
    }
}
