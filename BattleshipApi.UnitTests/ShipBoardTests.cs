using BattleshipApi.Core.Models;
using BattleshipApi.Common.Enums;
using BattleshipApi.Common.Models;

namespace BattleshipApi.UnitTests;

public class ShipBoardTests
{
    private readonly VesselBoard vesselBoard;

    public ShipBoardTests()
    {
        this.vesselBoard = new VesselBoard();
    }

    [Test]
    public void it_should_add_horizontal_ship_correctly()
    {
        // vesselBoard.AddVessel(new Coordinate(0, 0), VesselOrientation.Horizontal, 5);
        //
        // var tiles = vesselBoard.Tiles;

    }


    [Test]
    public void it_should_add_vertical_ship_correctly()
    {
        // var sut = new VesselBoard(new VesselFactory());
        //
        // sut.AddVessel(new Coordinate(0, 0), VesselOrientation.Vertical, 5);
        //
        // var vesselTiles = sut.Tiles.Where(_ => _.Type == TileType.Vessel).ToArray();
        //
        // Assert.AreEqual(6, vesselTiles.Count());
        // Assert.AreEqual(new Tile (0, 0, TileType.Vessel), vesselTiles[0]);
        // Assert.AreEqual(new Tile (0, 1, TileType.Vessel), vesselTiles[1]);
        // Assert.AreEqual(new Tile (0, 2, TileType.Vessel), vesselTiles[2]);
        // Assert.AreEqual(new Tile (0, 3, TileType.Vessel), vesselTiles[3]);
        // Assert.AreEqual(new Tile (0, 4, TileType.Vessel), vesselTiles[4]);
    }

    [Test]
    public void it_should_throw_VesselIntersectionException_if_intersection_exists()
    {
        var vessel1 = new Vessel("Vessel 1", 5);
        // var vessel2 = new Vessel("Vessel 2", 5);

        vesselBoard.AddVessel(new CartesianCoordinates(0, 0), VesselOrientation.Horizontal, vessel1);

        // Assert.Throws<VesselIntersectionException>(() =>
        //     vesselBoard.AddVessel(new Coordinate(0, 2), VesselOrientation.Horizontal, vessel2));

        Console.WriteLine(vesselBoard.ToString());
    }
}