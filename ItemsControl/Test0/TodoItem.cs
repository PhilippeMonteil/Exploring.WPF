using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test0
{

    public class TodoItem
    {
        // get requis pour le binding
        public string Title { get; set; }
        public int Completion { get; set; }

        public TodoItem(string Title, int Completion)
        {
            this.Title = Title;
            this.Completion = Completion;
        }

    }

}
