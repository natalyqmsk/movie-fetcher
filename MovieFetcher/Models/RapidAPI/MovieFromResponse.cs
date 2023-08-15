using System.Text.Json.Serialization;

namespace MovieFetcher.Models;

public class MovieFromResponse
{
    [JsonPropertyName("title")] 
    public string title { get; set; }
    
    [JsonPropertyName("year")] 
    public int year { get; set; }    
}