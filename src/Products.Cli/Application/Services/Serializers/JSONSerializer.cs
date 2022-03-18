namespace Products.Cli.Services.Serializers;

using Newtonsoft.Json.Linq;
using Products.Cli.Application.Abstractions;
using Products.Cli.Application.Utils;
using System.Text;
using System.Text.Json;

public class JSONSerializer : IJSONSerializer
{
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
        var jsonArray = obj[Constants.JSON_ARRAY_KEY].ToString();
        return jsonArray;
    }
}