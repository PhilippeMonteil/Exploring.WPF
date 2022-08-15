
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{

    internal class CTest0
    {
        public class MyEventArgs
        {
        }

        public delegate void MyEventHandler(object sender, MyEventArgs args);

        public event MyEventHandler? MyEvent;

        public void FireEvent()
        {
            if (MyEvent != null) MyEvent(this, new MyEventArgs());
        }

        public static void Test0()
        {
            CTest0 _pCTest0 = new CTest0();

            for (int i = 0; i < 3; i++)
            {
                int _index = i;
                _pCTest0.MyEvent += (sender, args) =>
                {
                    Console.WriteLine($"{nameof(MyEvent)} fired : receiver[{_index}] sender={sender} args={args}");
                };
            }

            _pCTest0.FireEvent();
        }

    }

}
