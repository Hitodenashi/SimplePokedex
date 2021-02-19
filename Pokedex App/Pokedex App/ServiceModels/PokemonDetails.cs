using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Pokedex_App.ServiceModels
{
    public struct DetailType
    {
        public string Slot { get; set; }
        public PokemonType Type { get; set; }
    }

    [DataContract]
    public class Sprites
    {
        [DataMember(Name = "back_female")]
        public string BackFemale { get; set; }
        
        [DataMember(Name = "back_shiny_female")]
        public string BackShinyFemale { get; set; }
        
        [DataMember(Name = "back_default")]
        public string BackDefault { get; set; }
        
        [DataMember(Name = "front_female")]
        public string FrontFemale { get; set; }
        
        [DataMember(Name = "front_shiny_female")]
        public string FrontShinyFemale { get; set; }
        
        [DataMember(Name = "back_shiny")]
        public string BackShiny { get; set; }
        
        [DataMember(Name = "front_default")]
        public string FrontDefault { get; set; }
        
        [DataMember(Name = "front_shiny")]
        public string FrontShiny { get; set; }
    }

    public class PokemonDetails
    {
        public string Name { get; set; } = "";
        public int ID { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }

        public Sprites Sprites { get; set; }
        public List<DetailType> Types { get; set; }
    }
}
