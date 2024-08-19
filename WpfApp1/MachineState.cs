using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class MachineState : IMachineState
    {
        public enum State
        {
            Save,
            NoSave
        }
        State _state { set;  get; }
        public bool SelectState(int state)
        {
          
            switch (state)
            {
                case 1:
                    _state = State.Save;
                    return true;
                case 0:
                    _state = State.NoSave;
                    return false;   
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
