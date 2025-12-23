namespace PokemonSearchWPF.Dtos;

public class PokemonDto
{
    public string Id { get; init; } = default!;

    public string Order { get; init; } = default!;

    public string Name { get; init; } = default!;

    public string Image { get; init; } = default!;

    public string Type { get; init; } = default!;

    public string Weight { get; set; } = default!;

    public string Height { get; init; } = default!;
}
