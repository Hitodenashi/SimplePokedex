using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pokedex_App.Views;

namespace Pokedex_App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Main();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
