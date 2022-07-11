using BattleshipApi.BusinessLogic.Models;

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
        
        sut.AddVessel(new Coordinate(0, 0), VesselOrientation.Horizontal, vessel);
        
        Assert.AreEqual(1, sut.Vessels.Count);
        Assert.AreEqual(vessel.Id, sut.Vessels.First().Id);
    }
    
    [Test]
    public void it_should_remove_vessel()
    {
        var sut = new Player("Douglas MacArthur");

        var vessel = new Vessel("Fake", 5);
        
        sut.AddVessel(new Coordinate(0, 0), VesselOrientation.Horizontal, vessel);
        
        sut.RemoveVessel(vessel.Id);
        
        Assert.AreEqual(0, sut.Vessels.Count);
    }
}