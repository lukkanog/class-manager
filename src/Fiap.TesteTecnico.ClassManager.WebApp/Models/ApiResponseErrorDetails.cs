using System.Text.Json.Serialization;

namespace Fiap.TesteTecnico.ClassManager.WebApp.Models;

public class ApiResponseErrorDetails
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;

    [JsonPropertyName("title")]
    public string Title { get; set; } = default!;

    [JsonPropertyName("status")]
    public int Status { get; set; } = default!;

    [JsonPropertyName("detail")]
    public string Detail { get; set; } = default!;

    [JsonPropertyName("errors")]
    public List<string> Errors { get; set; } = [];
}
