using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Pokedex_App.ServiceModels
{
    public class PokeClient
    {
        private static PokeClient _client;
        public static PokeClient Client => _client ?? (_client = new PokeClient());

        private PokeClient() { }

        static string Url = "https://pokeapi.co/api/v2/";
        static HttpClient client = new HttpClient();

        public async Task<List<PokemonListItem>> GetPokemon(int page = 0, int pageSize = 50)
        {
            Uri pokemonUri = new Uri(string.Format(Url + $"pokemon?limit={pageSize}&offset={page*pageSize}", string.Empty));

            var response = await SendRequest<BaseAPIResponse<PokemonListItem>>(pokemonUri);
            return response?.Results ?? null;
        }

        public async Task<PokemonDetails> GetPokemonDetails(string pokemonUrl)
        {
            Uri pokemonUri = new Uri(pokemonUrl);

            var response = await SendRequest<PokemonDetails>(pokemonUri);
            return response ?? null;
        }

        public async Task<List<PokemonType>> GetPokemonTypes()
        {
            Uri pokemonUri = new Uri(Url + "type?limit=25");

            var response = await SendRequest<BaseAPIResponse<PokemonType>>(pokemonUri);
            return response?.Results ?? null;
        }

        async Task<T> SendRequest<T>(Uri uri) where T : class
        {
            HttpResponseMessage response = await client.GetAsync(uri);
            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync(); 
                var details = JsonConvert.DeserializeObject<T>(content);
                return details;
            }

            return null;
        }
    }
}
