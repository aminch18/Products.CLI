namespace Products.Cli.Application.Dtos.Extensions;

public static class DTOExtensions
{
    public static ProductDTO ToProductDTO(this CapterraDTO dto)
        => new ProductDTO
        {
            Categories = dto.Tags.Split(",").ToList(),
            Name = dto.Name,
            Twitter = dto.Twitter
        };

    public static ProductDTO ToProductDTO(this SoftwareAdviceDTO dto)
        => new ProductDTO
        {
            Categories = dto.Categories,
            Name = dto.Title,
            Twitter = dto.Twitter
        };
}
