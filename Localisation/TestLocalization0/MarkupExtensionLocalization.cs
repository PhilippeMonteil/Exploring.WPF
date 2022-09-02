using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace TestLocalization0
{

    [MarkupExtensionReturnType(typeof(string))]
    public class MarkupExtensionLocalization : MarkupExtension
    {

        public MarkupExtensionLocalization(string id)
        {
            this.ID = id;
        }

        [ConstructorArgument("id")]
        public string ID { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return $"ID[{Thread.CurrentThread.CurrentUICulture.ToString()}]={ID}";
        }

    }

}
