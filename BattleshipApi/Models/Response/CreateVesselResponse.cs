namespace BattleshipApi.Models.Response;

public record CreateVesselResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int Damage { get; set; }
    public int Size { get; set; }
}