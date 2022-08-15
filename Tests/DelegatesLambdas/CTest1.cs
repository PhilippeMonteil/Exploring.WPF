using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    public class CTest1
    {

        #region --- Tests Variance

        // delegate covariant
        public delegate T Mydelegate1<out T>();

        // delegate non variant
        public delegate T Mydelegate2<T>(out T par0);
        // Error : public delegate T Mydelegate2A<out T>(out T par0);
        // Error : public delegate T Mydelegate2B<out T>(ref T par0);

        // delegate contravariant
        public delegate void Mydelegate3<in T>(T par0);

        static string method2(out string par0)
        {
            return par0 = "Test";
        }

        public static void Test1()
        {
            // delegate generic fermé
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

        #endregion


    }

}
