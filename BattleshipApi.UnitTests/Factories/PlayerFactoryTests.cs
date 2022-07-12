using BattleshipApi.Core.Factories;

namespace BattleshipApi.UnitTests.Factories;

public class PlayerFactoryTests
{
    [Test]
    public void it_should_create_player()
    {
        var sut = new PlayerFactory();

        var result = sut.Create("Joe");
        
        Assert.AreNotEqual(Guid.Empty, result.Id);
        Assert.AreEqual("Joe", result.Name);
        Assert.IsEmpty(result.Vessels);
        Assert.IsFalse(result.IsDefeated);
        Assert.IsNotNull(result.VesselBoard);
    }
    
    [Test]
    public void it_should_throw_exception_if_no_name_specified()
    {
        var sut = new PlayerFactory();
        
        Assert.Throws<ArgumentNullException>(() => sut.Create(string.Empty));
    }
}