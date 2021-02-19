using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pokedex_App.Droid
{
    [Activity(Label = "SplashScreen", MainLauncher=true, Theme="@style/SplashTheme", NoHistory=true, Icon="@drawable/PokeDexLogo")]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            StartActivity(typeof(MainActivity));
        }
    }
}