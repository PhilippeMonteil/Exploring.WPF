
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestCustomControl
{

    /// <summary>
    /// Interaction logic for MyUserControl.xaml
    /// </summary>
    /// 
    [ContentProperty(nameof(FileName))]
    public partial class MyUserControl : UserControl
    {
        static DependencyProperty FileNameProperty = DependencyProperty.Register(nameof(FileName),
                                                        typeof(string),
                                                        typeof(MyUserControl),
                                                        new PropertyMetadata(null, fileName_ChangedCallback));

        static void fileName_ChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as MyUserControl)?._fileName_ChangedCallback(e);
        }

        void _fileName_ChangedCallback(DependencyPropertyChangedEventArgs e)
        {
            // this.theTextBox.Text = e.NewValue as string;
        }

        public MyUserControl()
        {
            InitializeComponent();
        }

        public string FileName
        {
            get
            {
                return this.GetValue(FileNameProperty) as string;
            }
            set
            {
                this.SetValue(FileNameProperty, value);
            }
        }

        private void theButton_Click(object sender, RoutedEventArgs e)
        {
            this.FileName += "+";
        }
    }

}
