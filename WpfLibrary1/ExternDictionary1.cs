
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xml.Serialization;

[assembly:XmlnsDefinition("WpfLibrary1", "WpfLibrary1")]

namespace WpfLibrary1
{

    public partial class ExternDictionary1
    {
        static ExternDictionary1 s_Instance = new ExternDictionary1();
        public static ExternDictionary1 Instance => s_Instance;

        public ExternDictionary1()
        {
            InitializeComponent();
        }
    }

}
