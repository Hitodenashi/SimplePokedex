using Pokedex_App.ServiceModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Pokedex_App.ViewModels
{
    public class PokemonModel : BaseViewModel
    {
        private bool _loadingDetails;
        public bool LoadingDetails
        {
            get => _loadingDetails;
            set => SetProperty(ref _loadingDetails, value);
        }

        private string _url;
        public string Url
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private int _id;
        public int ID
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private int _weight;
        public int Weight
        {
            get => _weight;
            set => SetProperty(ref _weight, value);
        }

        private int _height;
        public int Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }

        private string _spriteUrl;
        public string SpriteUrl
        {
            get => _spriteUrl;
            set => SetProperty(ref _spriteUrl, value);
        }

        private ObservableCollection<PokemonType> _types;
        public ObservableCollection<PokemonType> Types
        {
            get => _types;
            set => SetProperty(ref _types, value);
        }

        public async void LoadDetails()
        {
            if (string.IsNullOrEmpty(Url)) return;

            LoadingDetails = true;

            var pokemonDetails = await PokeClient.Client.GetPokemonDetails(Url);

            ID = pokemonDetails.ID;
            Weight = pokemonDetails.Weight;
            Height = pokemonDetails.Height;
            SpriteUrl = pokemonDetails.Sprites.FrontDefault;

            Types = new ObservableCollection<PokemonType>();
            foreach (var type in pokemonDetails.Types)
                Types.Add(type.Type);

            LoadingDetails = false;
        }


        public static bool operator ==(PokemonModel first, PokemonModel second) => first?.ID == second?.ID;
        public static bool operator !=(PokemonModel first, PokemonModel second) => first?.ID != second?.ID;
        public override bool Equals(object obj) => this?.ID == ((PokemonModel)obj)?.ID;

        public override int GetHashCode()
        {
            int hashCode = -1519851154;
            hashCode = hashCode * -1521134295 + IsBusy.GetHashCode();
            hashCode = hashCode * -1521134295 + _loadingDetails.GetHashCode();
            hashCode = hashCode * -1521134295 + LoadingDetails.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_url);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Url);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + _id.GetHashCode();
            hashCode = hashCode * -1521134295 + ID.GetHashCode();
            hashCode = hashCode * -1521134295 + _weight.GetHashCode();
            hashCode = hashCode * -1521134295 + Weight.GetHashCode();
            hashCode = hashCode * -1521134295 + _height.GetHashCode();
            hashCode = hashCode * -1521134295 + Height.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_spriteUrl);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SpriteUrl);
            hashCode = hashCode * -1521134295 + EqualityComparer<ObservableCollection<PokemonType>>.Default.GetHashCode(_types);
            hashCode = hashCode * -1521134295 + EqualityComparer<ObservableCollection<PokemonType>>.Default.GetHashCode(Types);
            return hashCode;
        }
    }
}
