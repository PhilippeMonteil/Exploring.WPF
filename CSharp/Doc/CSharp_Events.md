
# C# Notes

## [Events](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/event)

### Exemple

    /**
     * - exemple avec surcharge r�entrante de add et remove  
    ***/
    internal class CTest0
    {
        public class MyEventArgs
        {
        }

        EventHandler<MyEventArgs>? m_MyEvent0;
        Object m_lock = new object();

        public event EventHandler<MyEventArgs> MyEvent0
        {
            add
            {
                lock(m_lock)
                {
                    m_MyEvent0 += value;
                }
            }
            remove
            {
                lock (m_lock)
                {
                    m_MyEvent0 -= value;
                }
            }
        }

        public void FireEvent()
        {
            if (m_MyEvent0 != null) m_MyEvent0(this, new MyEventArgs());
        }

        public static void Test0()
        {
            CTest0 _pCTest0 = new CTest0();

            for (int i = 0; i < 3; i++)
            {
                int _index = i;
                _pCTest0.MyEvent0 += (sender, args) =>
                {
                    Console.WriteLine($"{nameof(MyEvent0)} fired : receiver[{_index}] sender={sender} args={args}");
                };
            }

            _pCTest0.FireEvent();
        }

    }

