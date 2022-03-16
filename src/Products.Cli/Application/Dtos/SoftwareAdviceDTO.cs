namespace Products.Cli.Application.Dtos;

using System.Text.Json.Serialization;

public class SoftwareAdviceDTO
{
    public SoftwareAdviceDTO()
    {

    }

    public string ProviderName = "softwareadvice";

    [JsonPropertyName("categories")]
    public List<string> Categories { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("twitter")]
    public string Twitter { get; set; }
}