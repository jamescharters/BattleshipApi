namespace BattleshipApi.BusinessLogic.Models;

public class Tile : IEquatable<Tile>
{
    public TileType Type { get; set; }
    public Coordinate Coordinates { get; private set; }
    public ITileOccupant Occupant { get; set; }

    public Tile(int row, int column, TileType type = TileType.Water)
    {
        Type = type;
        Coordinates = new Coordinate(row, column);
    }

    public bool Equals(Tile? other)
    {
        if (other == null) return false;

        return Type == other.Type && Coordinates.Row == other.Coordinates.Row && Coordinates.Column ==
            other.Coordinates.Column;
    }
}