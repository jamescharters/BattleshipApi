using BattleshipApi.Core.Models;
using BattleshipApi.Common.Enums;
using BattleshipApi.Common.Exceptions;
using BattleshipApi.Common.Models;

namespace BattleshipApi.UnitTests;

public class VesselBoardTests
{
    [Test]
    public Task it_should_return_graphic_in_tostring()
    {
        var sut = new VesselBoard(10);

        var vessel = new Vessel("Fake", 5);

        sut.AddVessel(new Coordinate(0, 0), VesselOrientation.Horizontal, vessel);

        return Verifier.Verify(sut.ToString());
    }

    [Test]
    public Task it_should_return_graphic_in_tostring_multiple()
    {
        var sut = new VesselBoard(10);

        var vessel1 = new Vessel("Vessel 1", 5);
        var vessel2 = new Vessel("Vessel 2", 5);
        var vessel3 = new Vessel("Vessel 3", 3);

        sut.AddVessel(new Coordinate(0, 0), VesselOrientation.Horizontal, vessel1);
        sut.AddVessel(new Coordinate(1, 3), VesselOrientation.Horizontal, vessel2);
        sut.AddVessel(new Coordinate(6, 5), VesselOrientation.Vertical, vessel3);

        return Verifier.Verify(sut.ToString());
    }

    [Test]
    public void it_should_throw_exception_if_vessels_intersect()
    {
        var sut = new VesselBoard(10);

        var vessel1 = new Vessel("Vessel 1", 5);
        var vessel2 = new Vessel("Vessel 2", 5);

        sut.AddVessel(new Coordinate(0, 0), VesselOrientation.Horizontal, vessel1);

        Assert.Throws<VesselIntersectionException>(() =>
            sut.AddVessel(new Coordinate(4, 4), VesselOrientation.Vertical, vessel2));
    }

    [Test]
    public void it_should_throw_exception_if_vessel_already_placed()
    {
        var sut = new VesselBoard(10);

        var vessel1 = new Vessel("Vessel 1", 5);

        sut.AddVessel(new Coordinate(0, 0), VesselOrientation.Horizontal, vessel1);

        Assert.Throws<VesselAlreadyPlacedException>(() =>
            sut.AddVessel(new Coordinate(5, 4), VesselOrientation.Vertical, vessel1));
    }

    [Test]
    public void it_should_throw_exception_if_vessel_oob_xaxis()
    {
        var sut = new VesselBoard(10);

        var vessel1 = new Vessel("Vessel 1", 5);

        Assert.Throws<VesselOutOfBoundsException>(() =>
            sut.AddVessel(new Coordinate(0, 9), VesselOrientation.Horizontal, vessel1));
    }

    [Test]
    public void it_should_throw_exception_if_vessel_oob_yaxis()
    {
        var sut = new VesselBoard(10);

        var vessel1 = new Vessel("Vessel 1", 5);

        Assert.Throws<VesselOutOfBoundsException>(() =>
            sut.AddVessel(new Coordinate(0, 0), VesselOrientation.Vertical, vessel1));
    }

    [Test]
    public void it_should_throw_exception_if_board_full()
    {
        var sut = new VesselBoard(2);

        var vessel1 = new Vessel("Vessel 1", 2);
        var vessel2 = new Vessel("Vessel 2", 2);
        var vessel3 = new Vessel("Vessel 3", 2);

        sut.AddVessel(new Coordinate(0, 0), VesselOrientation.Horizontal, vessel1);
        sut.AddVessel(new Coordinate(1, 0), VesselOrientation.Horizontal, vessel2);

        Assert.Throws<BoardFullException>(() =>
            sut.AddVessel(new Coordinate(0, 0), VesselOrientation.Horizontal, vessel3));
    }
}