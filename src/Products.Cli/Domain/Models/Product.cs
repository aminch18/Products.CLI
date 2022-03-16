namespace Products.Cli.Domain.Models;

public class Product
{
    public Product(string name, List<string> categories, string twitter, Source source)
        : this(new Guid(), name, categories, twitter, source)
    {

    }

    protected Product(Guid id, string name, List<string> categories, string twitter, Source sourceProvider)
    {
        Id = id;
        Name = name;
        Categories = categories;
        Twitter = twitter;
        SourceProvider = sourceProvider;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string Category { get; private set; }

    public List<string> Categories { get; set; }

    public string Twitter { get; private set; }

    public Source SourceProvider { get; private set; }

    public static Product Build(string name,  List<string> categories, string twitter, Source source)
        => new(new(), name, categories, twitter, source);

    public override string ToString()
        => $"Name: \"{Name}\"; Categories: {string.Join(",", Categories)}; Twitter: {Twitter}";
}
