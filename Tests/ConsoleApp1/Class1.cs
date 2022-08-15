
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp1.Class1;

namespace ConsoleApp1
{

    public static class Class1
    {
        delegate int Mydelegate(int par0);

        static int method0(int par0)
        {
            return par0;
        }

        public static void Test0()
        {

            {
                Mydelegate d = method0;
                int v = d(100);
            }

            // delegate <- anonymous method
            {
                Mydelegate d = delegate (int par)
                {
                    return par * 10;
                };
                int v = d(100);
            }

            // delegate <- lambda expression
            {
                Mydelegate d = p => p * 100;
                int v = d(100);
            }

            {
                System.Linq.Expressions.Expression<Func<int, int>> e = x => x * x;
                Console.WriteLine(e);
                // Output:
                // x => (x * x)
            }
        }

        public delegate T Mydelegate1<out T>();

        public delegate T Mydelegate2<T>(out T par0);
        public delegate void Mydelegate3<in T>(T par0);

        // Errors:
        // public delegate T Mydelegate2<out T>(out T par0);
        // public delegate T Mydelegate3<out T>(ref T par0);
        // public delegate T Mydelegate4<out T>(T par0);

        static T method1<T>()
        {
            return default(T);
        }

        static string method2(out string par0)
        {
            return par0 = "Test";
        }

        public static void Test1()
        {

            {
                Mydelegate d = method0;
                int v = d(100);
            }

            {
                Mydelegate d = delegate (int par)
                {
                    return par * 10;
                };
                int v = d(100);
            }

            {
                Mydelegate d = p => p * 100;
                int v = d(100);
            }

            {
                Mydelegate2<string> d0 = method2;
            }

            // Variance
            {
                Mydelegate1<string> d1 = () => String.Empty;
                Mydelegate1<object> d0 = d1;
                object v = d0();
            }

            // Contravariance
            {
                Mydelegate3<object> d0 = null;
                Mydelegate3<string> d1 = d0;
            }

            {
                Mydelegate1<int> d = delegate ()
                {
                    return 777;
                };
            }
        }

    }

}
