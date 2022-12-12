using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova
{
    public class UserException
    {
        public UserException() {

            Error er = new Error();
            er.Show();
            System.Windows.Threading.Dispatcher.Run();
        }
        public UserException(string mess)
        {
            Error er = new Error();
            er.Show();
            System.Windows.Threading.Dispatcher.Run();
        }
    }
}
