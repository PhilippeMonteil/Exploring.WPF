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
