
# design time data context

	d:DataContext="{d:DesignInstance IsDesignTimeCreatable=True, Type=local:TodoItemListTest}"

# DataBinding : get accessor

    public class TodoItem
    {
        // get requis pour le data binding
        public string Title { get; init; }
        public int Completion { get; init; }

