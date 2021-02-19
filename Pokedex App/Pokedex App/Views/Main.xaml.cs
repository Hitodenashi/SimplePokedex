using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pokedex_App.ViewModels;

namespace Pokedex_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Main : ContentPage
    {
        public Main()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
            PokemonDetailsFrame.PropertyChanged += PokemonDetailsFrame_PropertyChanged;
        }

        private void PokemonDetailsFrame_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Frame.IsEnabledProperty.PropertyName)
                _ = UpdateDetailsVisibility();
        }

        private async Task UpdateDetailsVisibility()
        {
            if (PokemonDetailsFrame.IsEnabled)
            {
                PokemonDetailsFrame.IsVisible = true;
                await PokemonDetailsFrame.ScaleTo(1, 150);
            }
            else
            {
                await PokemonDetailsFrame.ScaleTo(0, 150);
                PokemonDetailsFrame.IsVisible = false;
            }
        }
    }
}