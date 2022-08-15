namespace Tests_Delegates
{

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

        #region --- Test0

        delegate int MyDelegate0(int p0);

        static int method0(int p0)
        {
            return p0;
        }

        static void Test0()
        {
            // delegate / method
            {
                MyDelegate0 d = method0;
                int v = d(7);
            }

            // delegate / méthode anonyme
            {
                MyDelegate0 d = delegate (int p0)
                {
                    return p0;
                };
                int v = d(77);
            }

            // delegate / lambda
            {
                MyDelegate0 d = (p0) => p0;
                int v = d(777);
            }
        }

        #endregion

    }

}
