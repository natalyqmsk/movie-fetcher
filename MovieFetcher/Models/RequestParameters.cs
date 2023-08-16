using System.Runtime.Serialization;
using Microsoft.Build.Framework;
using Newtonsoft.Json;

namespace MovieFetcher.Models;

public class RequestParameters
{
    [Required] public string Title { get; set; }
    public int? Page { get; set; } = 0;
    public int? PageSize { get; set; } = 10;
    public string? Sort { get; set; } = "moviemeter,asc";
}