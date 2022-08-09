using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test0
{

    public class TodoItemListTest : List<TodoItem>
    {

        public TodoItemListTest()
        {
            for (int i=0;i < 100; i++)
            {
                Add(new TodoItem($"TestItem0[{i}]", 90));
            }
        }

    }

}
