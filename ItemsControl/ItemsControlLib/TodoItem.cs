using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemsControlLib
{

    public class TodoItem
    {
        // get requis pour le data binding
        public string Title { get; init; }
        public int Completion { get; init; }

        public TodoItem(string Title, int Completion)
        {
            this.Title = Title;
            this.Completion = Completion;
        }

    }

}
