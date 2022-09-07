
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

[assembly: XmlnsDefinition("http://my.schemas.com/ResourceAssembly", "ResourceAssembly")]

namespace ResourceAssembly
{

    public partial class ExportResourceDictionary1
    {
        //Expose it as singleton to avoid multiple instances of this dictionary
        private static readonly ExportResourceDictionary1 _instance = new ExportResourceDictionary1();

        public static ExportResourceDictionary1 Instance => _instance;

        public ExportResourceDictionary1()
        {
            InitializeComponent();
        }
    }

}
