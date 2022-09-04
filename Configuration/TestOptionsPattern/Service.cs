
using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOptionsPattern
{

    public class Service
    {
        private readonly Features _personalizeFeature;
        private readonly Features _weatherStationFeature;

        public Service(IOptionsSnapshot<Features> namedOptionsAccessor)
        {
            _personalizeFeature = namedOptionsAccessor.Get(Features.Personalize);
            _weatherStationFeature = namedOptionsAccessor.Get(Features.WeatherStation);
        }

        public override string ToString()
        {
            return $"{GetType().Name}[{GetHashCode()}] _personalizeFeature={_personalizeFeature} _weatherStationFeature={_weatherStationFeature}";
        }

    }

}
