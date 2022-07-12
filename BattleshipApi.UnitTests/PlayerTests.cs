using BattleshipApi.Core.Models;
using BattleshipApi.Common.Enums;
using BattleshipApi.Common.Models;

namespace BattleshipApi.UnitTests;

public class PlayerTests
{
    [Test]
    public void it_should_throw_exception_if_name_is_empty()
    {
        Assert.Throws<ArgumentNullException>(() => new Player(string.Empty));
    }

    [Test]
    public void it_should_create_player()
    {
        var sut = new Player("Douglas MacArthur");

        Assert.AreEqual("Douglas MacArthur", sut.Name);
        Assert.AreNotEqual(Guid.Empty, sut.Id);
        Assert.IsEmpty(sut.Vessels);
        Assert.IsNotNull(sut.VesselBoard);
    }

    [Test]
    public void it_should_add_vessel()
    {
        var sut = new Player("Douglas MacArthur");

        var vessel = new Vessel("Fake", 5);

        sut.AddVessel(new CartesianCoordinates(0, 0), VesselOrientation.Horizontal, vessel);

        Assert.AreEqual(1, sut.Vessels.Count);
        Assert.AreEqual(vessel.Id, sut.Vessels.First().Id);
    }

    [Test]
    public void it_should_remove_vessel()
    {
        var sut = new Player("Douglas MacArthur");

        var vessel = new Vessel("Fake", 5);

        sut.AddVessel(new CartesianCoordinates(0, 0), VesselOrientation.Horizontal, vessel);

        sut.RemoveVessel(vessel.Id);

        Assert.AreEqual(0, sut.Vessels.Count);
    }

    [Test]
    public Task it_should_mark_misses()
    {
        var sut = new Player("Douglas MacArthur");

        var vessel = new Vessel("Fake", 5);

        sut.AddVessel(new CartesianCoordinates(0, 0), VesselOrientation.Horizontal, vessel);
        
        sut.FireAt(new CartesianCoordinates(2, 1));
        sut.FireAt(new CartesianCoordinates(3, 2));
        
        return Verifier.Verify(sut.VesselBoard.ToString());
    }
    
    [Test]
    public Task it_should_mark_hits()
    {
        var sut = new Player("Douglas MacArthur");

        var vessel = new Vessel("Fake", 5);

        sut.AddVessel(new CartesianCoordinates(0, 0), VesselOrientation.Horizontal, vessel);
        
        sut.FireAt(new CartesianCoordinates(0, 0));
        sut.FireAt(new CartesianCoordinates(0, 3));
        
        return Verifier.Verify(sut.VesselBoard.ToString());
    }
    
    [Test]
    public Task it_should_mark_hits_for_sunken_vessel()
    {
        var sut = new Player("Douglas MacArthur");

        var vessel = new Vessel("Fake", 5);

        sut.AddVessel(new CartesianCoordinates(0, 0), VesselOrientation.Horizontal, vessel);
        
        sut.FireAt(new CartesianCoordinates(0, 0));
        sut.FireAt(new CartesianCoordinates(0, 1));
        sut.FireAt(new CartesianCoordinates(0, 2));
        sut.FireAt(new CartesianCoordinates(0, 3));
        sut.FireAt(new CartesianCoordinates(0, 4));
        
        return Verifier.Verify(sut.VesselBoard.ToString());
    }
}