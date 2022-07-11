namespace BattleshipApi.Models;

public record class Coordinate
{
    /// <summary>
    /// Row
    /// </summary>
    public int X { get; set; }
    
    /// <summary>
    /// Column
    /// </summary>
    public int Y { get; set; }

    public Coordinate(int x, int y)
    {
        if (x < 0) throw new ArgumentOutOfRangeException();
        if (y < 0) throw new ArgumentOutOfRangeException();

        X = x;
        Y = y;
    }
}