using System.Text;
using BattleshipApi.BusinessLogic.Exceptions;
using BattleshipApi.BusinessLogic.Factories;

namespace BattleshipApi.BusinessLogic.Models;

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

    public Tile? TileAt(Coordinate coordinate)
    {
        return TileAt(coordinate.Row, coordinate.Column);
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

    public void AddVessel(Coordinate start, VesselOrientation vesselOrientation, Vessel vessel)
    {
        var proposedVesselTiles = new List<Tile>();

        if (findTilesByOccupantId(vessel.Id).Count > 0)
        {
            throw new VesselAlreadyPlacedException($"Vessel {vessel.Name} ({vessel.Id}) already placed!");
        }

        // We should have a sensible length
        if (vessel.Size <= 0) throw new ArgumentOutOfRangeException();

        // We should not try to go out of bounds
        if (vesselOrientation == VesselOrientation.Horizontal && start.Row + vessel.Size > MaxX)
            throw new Exception("Cannot place Vessel: overflow on X-axis");
        if (vesselOrientation == VesselOrientation.Vertical && start.Column + vessel.Size > MaxY)
            throw new Exception("Cannot place Vessel: overflow on Y-axis");

        // TODO: refactor this
        if (vesselOrientation == VesselOrientation.Horizontal)
        {
            var x = Tiles.GetLength(0) - start.Row - 1;

            for (var tick = 0; tick < vessel.Size; tick++)
            {
                proposedVesselTiles.Add(new Tile(x, tick + start.Column, TileType.Vessel));
                // proposedVesselTiles.Add( TileAt(new Coordinate(x, tick + start.Column)));
            }
        }

        if (vesselOrientation == VesselOrientation.Vertical)
        {
            for (var tick = 0; tick < vessel.Size; tick++)
            {
                proposedVesselTiles.Add(new Tile(tick + start.Row, start.Column, TileType.Vessel));
                // proposedVesselTiles.Add( TileAt(new Coordinate(tick + start.Row, start.Column)));
            }
        }

        // Now we have a proposed set of tiles for this vessel, check if it intersects with another vessel
        var currentVesselTiles = findTilesByType(TileType.Vessel);

        // Sanity check: is the entire grid crammed full of vessels?
        if (currentVesselTiles.Count == Tiles.Length)
            throw new BoardFullException("Current board is fully occupied");

        foreach (var occupiedTile in currentVesselTiles)
        {
            var converted = TileAt(occupiedTile.Coordinates);
            if (proposedVesselTiles.Any(computedTile => converted.Coordinates.Row == converted.Coordinates.Row &&
                                                        converted.Coordinates.Column ==
                                                        converted.Coordinates.Column))
            {
                throw new VesselIntersectionException(
                    $"Vessel intersects tile covered by vessel {occupiedTile.Occupant.Name}!");
            }
        }

        // If we are in the clear, associate ShipBoard tiles with the new vessel (to help us find hits/misses later)

        foreach (var proposedTile in proposedVesselTiles)
        {
            var vesselTile = TileAt(proposedTile.Coordinates);
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