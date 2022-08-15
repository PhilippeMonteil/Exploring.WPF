using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    public class CTest2
    {

        public static async Task<int> Test0()
        {
            Func<Task<int>> d = async () =>
            {
                await Task.Delay(1000);
                return 1;
            };
            //
            return await d();
        }

    }

}
