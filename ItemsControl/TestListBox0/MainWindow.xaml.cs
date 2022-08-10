using ItemsControlLib;
using System.Windows;

namespace TestListBox0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new TodoItemListTest();
        }
    }
}
