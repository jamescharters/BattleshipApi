using BattleshipApi.Core.Interfaces;

namespace BattleshipApi.Core.Models;

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
    /// Corresponds to of the vessel in virtual "squares" of the board, i.e. how many hits it can take before sinking
    /// </summary>
    public int Size { get; set; }

    public bool IsDead => Damage >= Size;

    public TileType Type => TileType.Vessel;

    public Vessel(string name, int size)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException($"{nameof(name)}");
        if (size <= 0) throw new ArgumentOutOfRangeException($"{nameof(size)}");

        Id = Guid.NewGuid();
        Damage = 0;
        Name = name;
        Size = size;
    }

    public void AddDamage(int amount = 1)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException();

        if (Damage + amount == Size)
        {
            Damage = amount;
            return;
        }

        Console.WriteLine($"Vessel {Name} took {amount} damage!");

        Damage += amount;
    }
}