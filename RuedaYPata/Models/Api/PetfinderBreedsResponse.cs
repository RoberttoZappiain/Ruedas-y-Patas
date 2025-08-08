using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RuedaYPata.Models.Api
{
    public class PetfinderBreedsResponse
    {
        [JsonPropertyName("breeds")]
        public List<Breed> Breeds { get; set; }
    }

    public class Breed
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}