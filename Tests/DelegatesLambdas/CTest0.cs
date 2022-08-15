
namespace ConsoleApp1
{

    public static class CTest0
    {

        #region --- Test0

        delegate int Mydelegate0(int par0);

        static int method0(int par0)
        {
            return par0;
        }

        public static void Test0()
        {

            {
                Mydelegate0 d = method0;
                int v = d(100);
            }

            // delegate <- anonymous method
            {
                Mydelegate0 d = delegate (int par)
                {
                    return par * 10;
                };
                int v = d(100);
            }

            // delegate <- lambda expression
            {
                Mydelegate0 d = p => p * 100;
                int v = d(100);
            }

            {
                System.Linq.Expressions.Expression<Func<int, int>> e = x => x * x;
                Console.WriteLine(e);
                // Output:
                // x => (x * x)
            }
        }

        #endregion

    }

}
