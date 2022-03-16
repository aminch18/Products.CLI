namespace Products.Cli.Services;

using Newtonsoft.Json.Linq;
using Products.Cli.Application.Abstractions;
using System.Text;
using System.Text.Json;

public class JSONSerializer : IJSONSerializer
{
    const string ARRAY_NAME = "products";
    public JSONSerializer()
    {

    }

    public T Deserialize<T>(string jsonstring)
        => JsonSerializer.Deserialize<T>(FixJson(jsonstring));

    public async Task<T> DeserializeAsync<T>(Stream input)
        => await JsonSerializer.DeserializeAsync<T>(input);


    public async Task<T> DeserializeAsync<T>(string jsonstring)
    {
        var jsonArray = FixJson(jsonstring);
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonArray));
        return await JsonSerializer.DeserializeAsync<T>(stream);
    }

    private string FixJson(string jsonstring)
    {
        JObject obj = JObject.Parse(jsonstring);
        var jsonArray = obj[ARRAY_NAME].ToString();
        return jsonArray;
    }
}