namespace BattleshipApi.Models;

public class Vessel
{
    /// <summary>
    /// A random Id for the vessel
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// A friendly name for the vessel
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Number of hits
    /// </summary>
    public int Damage { get; set; }
    
    /// <summary>
    /// Length of the vessel in virtual "squares" of the board
    /// </summary>
    public int Size { get; set; }

    public OccupationType Type { get; set; }

    public bool IsDead => Damage > Size;
}