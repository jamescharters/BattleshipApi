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
        Assert.IsNotNull(sut.ShotBoard);
    }
}