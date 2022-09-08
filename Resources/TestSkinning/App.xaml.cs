
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.Versioning;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows;

namespace TestSkinning
{

    public enum Skin { Red, Blue }

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static Skin s_Skin = Skin.Blue;

        public static Skin Skin
        {
            get => s_Skin;
            set
            {
                if (s_Skin == value) return;
                s_Skin = value;
            }
        }

        public void UpdateSkin()
        {
            SetSkin(Skin == Skin.Blue ? Skin.Red : Skin.Blue);
        }

        public void SetSkin(Skin skin)
        {
            Skin = skin;
            foreach (ResourceDictionary rd in this.Resources.MergedDictionaries)
            {
                if (rd is SkinResourceDictionary sd)
                {
                    sd.UpdateSource(skin);
                }
            }
        }

    }

}
