using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace semestralka_scholzova.Model
{
    public class ReadeFromConsole
    {


        public string reade()
        {
           
            
          
             Input chldWindow = new Input();
             chldWindow.Show();
           
            System.Windows.Threading.Dispatcher.Run();


             string text = chldWindow.inputText;






            return text;
        }



    }
}
