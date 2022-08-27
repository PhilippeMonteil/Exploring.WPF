using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace TestMenus0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Debug.WriteLine($"NewCommand_CanExecute");
            e.CanExecute = true;
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Debug.WriteLine($"NewCommand_Executed");
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Debug.WriteLine($"CommandBinding_CanExecute");
            e.CanExecute = true;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Debug.WriteLine($"CommandBinding_Executed");
        }

    }
}
