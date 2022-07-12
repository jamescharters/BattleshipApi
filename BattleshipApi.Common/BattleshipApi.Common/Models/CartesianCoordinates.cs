namespace BattleshipApi.Common.Models;

public record class CartesianCoordinates
{
    /// <summary>
    /// Row
    /// </summary>
    public int Row { get; set; }

    /// <summary>
    /// Column
    /// </summary>
    public int Column { get; set; }

    public CartesianCoordinates(int row, int column)
    {
        if (row < 0) throw new ArgumentOutOfRangeException($"{nameof(row)}");
        if (column < 0) throw new ArgumentOutOfRangeException($"{nameof(column)}");

        Row = row;
        Column = column;
    }

    public override string ToString()
    {
        return $"(Row: {Row}, Column: {Column})";
    }
}