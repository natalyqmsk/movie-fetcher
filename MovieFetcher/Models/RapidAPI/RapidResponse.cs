using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace MovieFetcher.Models;

public class RapidResponse
{
    [JsonPropertyName("results")] 
    public IEnumerable<MovieFromResponse> results { get; set; }
}