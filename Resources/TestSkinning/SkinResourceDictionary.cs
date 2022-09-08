using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestSkinning
{

    public class SkinResourceDictionary : ResourceDictionary
    {
        private Uri _redSource;
        private Uri _blueSource;

        public Uri RedSource
        {
            get { return _redSource; }
            set
            {
                _redSource = value;
                updateSource();
            }
        }
        public Uri BlueSource
        {
            get { return _blueSource; }
            set
            {
                _blueSource = value;
                updateSource();
            }
        }

        private void updateSource()
        {
            UpdateSource(App.Skin);
        }

        public void UpdateSource(Skin skin)
        {
            var val = skin == Skin.Red ? RedSource : BlueSource;
            if (val != null && base.Source != val)
            {
                base.Source = val;
            }
        }

    }

}
