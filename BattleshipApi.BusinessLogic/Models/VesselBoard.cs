using System.Text;
using BattleshipApi.BusinessLogic.Exceptions;
using BattleshipApi.BusinessLogic.Factories;

namespace BattleshipApi.BusinessLogic.Models;

public class VesselBoard
{
    private readonly IVesselFactory vesselFactory;

    public Tile[,] Tiles { get; set; }

    protected readonly int MaxX;
    protected readonly int MaxY;

    public VesselBoard(IVesselFactory vesselFactory, int size = 10)
    {
        if (size <= 0) throw new ArgumentOutOfRangeException();

        this.vesselFactory = vesselFactory;

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

    public Tile TileAt(Coordinate coordinate)
    {
        return TileAt(coordinate.Row, coordinate.Column);
    }

    public Tile TileAt(int row, int column)
    {
        return Tiles[row, column];
    }

    public void AddVessel(Coordinate start, VesselOrientation vesselOrientation, int length)
    {
        var proposedVesselTiles = new List<Tile>();

        // We should have a sensible length
        if (length <= 0) throw new ArgumentOutOfRangeException();

        // We should not try to go out of bounds
        if (vesselOrientation == VesselOrientation.Horizontal && start.Row + length > MaxX)
            throw new Exception("Cannot place Vessel: overflow on X-axis");
        if (vesselOrientation == VesselOrientation.Vertical && start.Column + length > MaxY)
            throw new Exception("Cannot place Vessel: overflow on Y-axis");

        // Add the starting point tile to the proposed list
        // proposedVesselTiles.Add(new Tile(start.X, start.Y, TileType.Vessel));

        // Compute the remainder of the set of tiles that represent the vessel to be added.
        // for (var tick = 0; tick < length; tick++)
        // {
        //     proposedVesselTiles.Add(new Tile(
        //         vesselOrientation == VesselOrientation.Horizontal ? start.X : start.X + tick,
        //         vesselOrientation == VesselOrientation.Vertical ? start.Y : start.Y + tick, TileType.Vessel));
        // }
        
        // proposedVesselTiles.Add(new Tile(Tiles.GetLength(0) - start.X, ));
        // For a 10 * 10 grid, the bottom left tile is X=10, Y=0
    // proposedVesselTiles.Add(new Tile(Tiles.GetLength(0) - 1, 0, TileType.Vessel));
    // proposedVesselTiles.Add(new Tile(Tiles.GetLength(0) - 2, 1, TileType.Vessel));
    // proposedVesselTiles.Add(new Tile(Tiles.GetLength(0) - 3, 2, TileType.Vessel));
       
    // * * *
    // * * *
    // * S *
    
    // this would be array[2,1]
    // what I want is row 0 to be bottom, 
    
    
    
    // proposedVesselTiles.Add(new Tile(Tiles.GetLength(0) - start.Y - 1,Tiles.GetLength(0) - start.X - 1, TileType.Vessel));
   

    if (vesselOrientation == VesselOrientation.Horizontal)
    {
        var x = Tiles.GetLength(0) - start.Row - 1;
        // var y = Tiles.GetLength(1) - start.Column - 1;
        
        for (var tick = 0; tick < length; tick++)
        {
            
            proposedVesselTiles.Add(new Tile(x, tick, TileType.Vessel));
        }
    }
    
    if (vesselOrientation == VesselOrientation.Vertical)
    {
        // var x = Tiles.GetLength(0) - start.Row - 1;
        var y = Tiles.GetLength(1) - start.Column - 1;
        
        for (var tick = 0; tick < length; tick++)
        {
            proposedVesselTiles.Add(new Tile(tick, y, TileType.Vessel));
        }
    }
    
    
        // Now we have a proposed set of tiles for this vessel, check if it intersects with another vessel
        var currentVesselTiles = findTilesByType(TileType.Vessel);

        // Sanity check: is the entire grid crammed full of vessels?
        if (currentVesselTiles.Count == Tiles.Length)
            throw new BoardFullException("Current board is fully occupied");

        foreach (var occupiedTile in currentVesselTiles)
        {
            if (proposedVesselTiles.Any(computedTile => occupiedTile.Coordinates.Row == computedTile.Coordinates.Row &&
                                                        occupiedTile.Coordinates.Column == computedTile.Coordinates.Column))
            {
                throw new VesselIntersectionException(
                    $"Vessel intersects tile covered by vessel {occupiedTile.Occupant.Name}!");
            }
        }

        // If we are in the clear, associate ShipBoard tiles with the new vessel (to help us find hits/misses later)
        var newVessel = vesselFactory.Create();

        foreach (var proposedTile in proposedVesselTiles)
        {
            var vesselTile = TileAt(proposedTile.Coordinates);
            vesselTile.Type = TileType.Vessel;
            vesselTile.Occupant = newVessel;
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

    #endregion
}