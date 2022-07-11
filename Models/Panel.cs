namespace BattleshipApi.Models;

public class Panel
{
    public OccupationType Type { get; private set; }
    public Coordinate Coordinates { get; private set; }

    public Panel(int row, int column)
    {
        Type = OccupationType.None;
        Coordinates = new Coordinate(row, column);
    }
}