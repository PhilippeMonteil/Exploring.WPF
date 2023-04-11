using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestCustomControl
{

    public class Class1 : IDisposable
    {
        private bool disposedValue;

        public Class1()
        {
            Debug.WriteLine($"Class1[{GetHashCode()}]");
        }

        public void FileNameChanged(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"Class1[{GetHashCode()}].FileNameChanged");
        }

        protected virtual void Dispose(bool disposing)
        {
            Debug.WriteLine($"Class1[{GetHashCode()}].Dispose(disposing={disposing})");
        }

        // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~Class1()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

}
