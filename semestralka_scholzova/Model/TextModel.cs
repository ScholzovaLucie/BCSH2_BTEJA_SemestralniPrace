using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace semestralka_scholzova.Model
{
    public class TextModel
    {
    }

    public class InputText : INotifyPropertyChanged
    {
        public string text;
        public MyICommand DoneCommand { get; set; }
       

        public InputText()
        {
 DoneCommand = new MyICommand(doneClick);
        }


        public string TextArea
        {
            get { return text; }

            set
            {
                if (text != value)
                {
                    text = value;
                    RaisePropertyChanged("ConsoleText");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private void doneClick()
        {
            text = text;
            System.Windows.Threading.Dispatcher.Yield();
        }
    }
}
