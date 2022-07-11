namespace BattleshipApi.BusinessLogic.Models;

public class Vessel : ITileOccupant
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

    // public TileOccupationType Type { get; set; }

    public bool IsDead => Damage >= Size;

    public TileType Type => TileType.Vessel;
    //
    // public List<Tile> Tiles { get; set; } = new();
    //
    //
    // public Vessel(List<Tile> tiles)
    // {
    //     Tiles = tiles;
    // }
    
    public void AddDamage(int amount = 1)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException();

        if (Damage + amount == Size)
        {
            Damage = Size;
            return;
        }
        
        Damage += amount;
    }
}