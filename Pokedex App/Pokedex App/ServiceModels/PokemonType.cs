using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Pokedex_App.ServiceModels
{
    public class PokemonType
    {
        public string Name { get; set; }
        public string Url { get; set; } = "";
        public Color TypeColor => GetTypeColor();


        private Color GetTypeColor()
        {
            switch (Name)
            {
                case "normal":
                    return Color.FromHex("#A8A878");
                case "fighting":
                    return Color.FromHex("#C03028");
                case "flying":
                    return Color.FromHex("#A890F0");
                case "poison":
                    return Color.FromHex("#A040A0");
                case "ground":
                    return Color.FromHex("#E0C068");
                case "rock":
                    return Color.FromHex("#B8A038");
                case "bug":
                    return Color.FromHex("#A8B820");
                case "ghost":
                    return Color.FromHex("#705898");
                case "steel":
                    return Color.FromHex("#B8B8D0");
                case "fire":
                    return Color.FromHex("#F08030");
                case "water":
                    return Color.FromHex("#6890F0");
                case "grass":
                    return Color.FromHex("#78C850");
                case "electric":
                    return Color.FromHex("#F8D030");
                case "psychic":
                    return Color.FromHex("#F85888");
                case "ice":
                    return Color.FromHex("#98D8D8");
                case "dragon":
                    return Color.FromHex("#7038F8");
                case "dark":
                    return Color.FromHex("#705848");
                case "fairy":
                    return Color.FromHex("#EE99AC");
                case "shadow":
                    return Color.FromHex("#404040");
                case "unknown":
                    return Color.FromHex("#68A090");
                default:
                    return Color.White;
            }
        }

        public static bool operator ==(PokemonType first, PokemonType second) => first?.Name == second?.Name;
        public static bool operator !=(PokemonType first, PokemonType second) => first?.Name != second?.Name;
        public override bool Equals(object obj) => this?.Name == ((PokemonType)obj)?.Name;

        public override int GetHashCode()
        {
            int hashCode = -1254404684;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Url);
            return hashCode;
        }
    }
}
