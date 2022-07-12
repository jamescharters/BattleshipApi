namespace BattleshipApi.Models.Response;

public class AddVesselResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int Damage { get; set; }
    public int Size { get; set; }
}