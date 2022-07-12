using System.Text;
using BattleshipApi.Common.Enums;
using BattleshipApi.Common.Exceptions;
using BattleshipApi.Common.Models;

namespace BattleshipApi.Core.Models;

public class VesselBoard
{
    public Tile[,] Tiles { get; set; }

    protected readonly int MaxX;
    protected readonly int MaxY;

    public VesselBoard(int size = 10)
    {
        if (size <= 0) throw new ArgumentOutOfRangeException();

        Tiles = new Tile[size, size];

        for (var row = 0; row < size; row++)
        {
            for (var column = 0; column < size; column++)
            {
                Tiles[row, column] = new Tile(row, column);
            }
        }

        MaxX = MaxY = size;
    }

    public Tile? TileAt(CartesianCoordinates cartesianCoordinates)
    {
        return TileAt(cartesianCoordinates.Row, cartesianCoordinates.Column);
    }

    public Tile? TileAt(int row, int column)
    {
        // return Tiles[Tiles.GetLength(0) - row - 1, Tiles.GetLength(0) + column];
        // Tile [0, 0] is the bottom left hand corner of the grid.

        // return Tiles[Tiles.GetLength(0) - row - 1, Tiles.GetLength(0) + column];

        var x = Tiles.GetLength(0) - 1;
        var y = Tiles.GetLength(1) - 1;

        var xPos = x - row;
        if (xPos < 0 || column < 0) return null;

        return Tiles[xPos, column];
    }

    public void RemoveVessel(Guid vesselId)
    {
        var associatedTiles = findTilesByOccupantId(vesselId);

        foreach (var tile in associatedTiles)
        {
            tile.Occupant = null;
        }
    }

    public void AddVessel(CartesianCoordinates start, VesselOrientation vesselOrientation, Vessel vessel)
    {
        if (vessel.Size <= 0) throw new ArgumentOutOfRangeException();

        // We should not try to go out of bounds
        if (vesselOrientation == VesselOrientation.Horizontal && start.Column + vessel.Size > MaxX)
            throw new VesselOutOfBoundsException("Cannot place Vessel: overflow on X-axis");

        if (vesselOrientation == VesselOrientation.Vertical && MaxY - start.Row - 1 + vessel.Size > MaxY)
            throw new VesselOutOfBoundsException("Cannot place Vessel: overflow on Y-axis");

        // We can't place the same vessel more than once
        if (findTilesByOccupantId(vessel.Id).Count > 0)
            throw new VesselAlreadyPlacedException($"Vessel {vessel.Name} ({vessel.Id}) already placed!");

        var proposedVesselTiles = new List<Tile>();

        // Horizontal ships start at the (x,y) co-ordinate then count "rightwards" from that point (i.e. column++)
        if (vesselOrientation == VesselOrientation.Horizontal)
        {
            var xPos = Tiles.GetLength(0) - start.Row - 1;

            for (var tileIndex = 0; tileIndex < vessel.Size; tileIndex++)
            {
                proposedVesselTiles.Add(new Tile(xPos, tileIndex + start.Column, TileType.Vessel));
            }
        }

        // Vertical ships start at the (x,y) co-ordinate then count "downwards" from that point (i.e. row++)
        if (vesselOrientation == VesselOrientation.Vertical)
        {
            var yPos = Tiles.GetLength(1);

            for (var tileIndex = 0; tileIndex < vessel.Size; tileIndex++)
            {
                proposedVesselTiles.Add(new Tile((yPos - start.Row - 1) + tileIndex, start.Column, TileType.Vessel));
            }
        }

        // Check for overlaps
        var currentVesselTiles = findTilesByType(TileType.Vessel);

        // Is the entire grid crammed full of vessels?
        if (currentVesselTiles.Count == Tiles.Length)
            throw new BoardFullException("Current board is fully occupied");

        foreach (var propTile in proposedVesselTiles)
        {
            foreach (var occupiedTile in currentVesselTiles)
            {
                if (propTile.CartesianCoordinateses.Equals(occupiedTile.CartesianCoordinateses))
                {
                    throw new VesselIntersectionException(
                        $"Vessel {vessel.Name} intersects tile occupied by vessel {occupiedTile.Occupant.Name} at {propTile.CartesianCoordinateses}!");
                }
            }
        }

        foreach (var proposedTile in proposedVesselTiles)
        {
            var vesselTile = Tiles[proposedTile.CartesianCoordinateses.Row, proposedTile.CartesianCoordinateses.Column];
            vesselTile.Type = TileType.Vessel;
            vesselTile.Occupant = vessel;
        }
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        for (var row = 0; row < Tiles.GetLength(0); row++)
        {
            for (var column = 0; column < Tiles.GetLength(1); column++)
            {
                var tile = Tiles[row, column];

                switch (tile.Type)
                {
                    case TileType.Hit:
                        {
                            stringBuilder.Append(" + ");
                            break;
                        }
                    case TileType.Miss:
                        {
                            stringBuilder.Append(" - ");
                            break;
                        }
                    case TileType.Vessel:
                        {
                            stringBuilder.Append(" V ");
                            break;
                        }
                    case TileType.Water:
                        {
                            stringBuilder.Append(" * ");
                            break;
                        }
                }
            }

            stringBuilder.AppendLine();
        }

        return stringBuilder.ToString();
    }

    #region Private

    private List<Tile> findTilesByType(TileType tileType)
    {
        var matchingTiles = new List<Tile>();

        // DEVNOTE: we assume the tile grid is always square
        var size = Tiles.GetLength(0);

        for (var row = 0; row < size; row++)
        {
            for (var column = 0; column < size; column++)
            {
                if (Tiles[row, column].Type == tileType)
                {
                    matchingTiles.Add(Tiles[row, column]);
                }
            }
        }

        return matchingTiles;
    }

    private List<Tile> findTilesByOccupantId(Guid occupantId)
    {
        var matchingTiles = new List<Tile>();

        // DEVNOTE: we assume the tile grid is always square
        var size = Tiles.GetLength(0);

        for (var row = 0; row < size; row++)
        {
            for (var column = 0; column < size; column++)
            {
                if (Tiles[row, column].Occupant != null && Tiles[row, column].Occupant.Id == occupantId)
                {
                    matchingTiles.Add(Tiles[row, column]);
                }
            }
        }

        return matchingTiles;
    }

    #endregion
}