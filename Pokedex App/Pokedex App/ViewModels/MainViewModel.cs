using Pokedex_App.ServiceModels;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System;

namespace Pokedex_App.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public int PageSize => 25;

        private bool _loadingPokemonDetails;
        public bool LoadingPokemonDetails
        {
            get => _loadingPokemonDetails;
            set => SetProperty(ref _loadingPokemonDetails, value);
        }

        private int _itemThreshold = 1;
        public int ItemThreshold
        {
            get => _itemThreshold;
            set => SetProperty(ref _itemThreshold, value);
        }

        private ObservableCollection<PokemonType> _pokemonTypes = new ObservableCollection<PokemonType>();
        public ObservableCollection<PokemonType> PokemonTypes
        {
            get => _pokemonTypes;
            set => SetProperty(ref _pokemonTypes, value);
        }

        private PokemonType _selectedTypeFilter;
        public PokemonType SelectedTypeFilter
        {
            get => _selectedTypeFilter;
            set
            {
                SetProperty(ref _selectedTypeFilter, value);
                FilterPokemonByType(true);
            }
        }

        private ObservableCollection<PokemonModel> _pokemonlist = new ObservableCollection<PokemonModel>();

        private ObservableCollection<PokemonModel> _filteredPokemonList = new ObservableCollection<PokemonModel>();
        public ObservableCollection<PokemonModel> FilteredPokemonList
        {
            get => _filteredPokemonList;
            set => SetProperty(ref _filteredPokemonList, value);
        }

        private bool _PokemonDetailsVisible;
        public bool PokemonDetailsVisible
        {
            get => _PokemonDetailsVisible;
            set => SetProperty(ref _PokemonDetailsVisible, value);
        }

        private PokemonModel _selectedPokemon;
        public PokemonModel SelectedPokemon
        {
            get => _selectedPokemon;
            set
            {
                SetProperty(ref _selectedPokemon, value);
                if (value != null) ShowPokemonDetails();
            }
        }

        private PokemonDetails _selectedPokemonDetails;
        public PokemonDetails SelectedPokemonDetails
        {
            get => _selectedPokemonDetails;
            set => SetProperty(ref _selectedPokemonDetails, value);
        }

        public ICommand HidePokemonDetailsCommand => new Command(() => HidePokemonDetails());
        public ICommand LoadNextPageCommand => new Command(() => LoadMorePokemon());

        public MainViewModel()
        {
            PokemonDetailsVisible = false;

            LoadMorePokemon();
            LoadPokemonTypes();
        }

        async void LoadMorePokemon()
        {
            if (IsBusy) return;

            IsBusy = true;

            var page = _pokemonlist.Count / PageSize;
            var newPokemon = await PokeClient.Client.GetPokemon(page, PageSize);
            if (newPokemon.Count == 0)
            {
                IsBusy = false;
                ItemThreshold = -1;
                return;
            }

            foreach (var Entry in newPokemon)
            {
                _pokemonlist.Add(new PokemonModel { Name = Entry.Name, Url = Entry.Url });
                _pokemonlist.Last().LoadDetails();
            }

            FilterPokemonByType();
            IsBusy = false;
        }

        async void LoadPokemonTypes()
        {
            var defaultType = new PokemonType { Name = "All" };
            PokemonTypes.Add(defaultType);

            var types = await PokeClient.Client.GetPokemonTypes();
            if(types.Count > 0)
            {
                foreach (var type in types)
                    PokemonTypes.Add(type);
            }

            OnPropertyChanged(nameof(PokemonTypes));
            SelectedTypeFilter = PokemonTypes.First();
        }

        void ShowPokemonDetails()
        {
            PokemonDetailsVisible = true;
        }

        void HidePokemonDetails()
        {
            PokemonDetailsVisible = false;
            SelectedPokemon = null;
        }

        void FilterPokemonByType(bool FilterChanged = false)
        {
            if (SelectedTypeFilter == null || SelectedTypeFilter == PokemonTypes.First())
            {
                FilteredPokemonList = _pokemonlist;
            }
            else
            {
                if (FilterChanged)
                    FilteredPokemonList = new ObservableCollection<PokemonModel>();
                foreach (var pokemon in _pokemonlist)
                {
                    if (pokemon.Types?.Contains(SelectedTypeFilter) == true)
                    {
                        if (!FilteredPokemonList.Contains(pokemon))
                            FilteredPokemonList.Add(pokemon);
                    }
                }
            }
        }
    }
}
