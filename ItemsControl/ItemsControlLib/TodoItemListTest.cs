using System.Collections.Generic;

namespace ItemsControlLib
{

    public class TodoItemListTest : List<TodoItem>
    {

        public TodoItemListTest()
        {
            for (int i = 0; i < 100; i++)
            {
                Add(new TodoItem($"TestItem0[{i}]", 90));
            }
        }

    }

}
