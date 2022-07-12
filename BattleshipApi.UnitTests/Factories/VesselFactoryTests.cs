using BattleshipApi.Core.Factories;
using BattleshipApi.Core.Models;

namespace BattleshipApi.UnitTests.Factories;

public class VesselFactoryTests
{
    [Test]
    public void it_should_create_vessel()
    {
        var sut = new VesselFactory();

        var result = sut.Create(5);
        
        Assert.AreEqual(0, result.Damage);
        Assert.AreNotEqual(Guid.Empty, result.Id);
        Assert.IsNotEmpty(result.Name);
        Assert.AreEqual(5, result.Size);
        Assert.AreEqual(TileType.Vessel, result.Type);
        Assert.IsFalse(result.IsDead);
    }
    
    [Test]
    public void it_should_throw_exception_if_size_eq_zero()
    {
        var sut = new VesselFactory();

        Assert.Throws<ArgumentOutOfRangeException>(() => sut.Create(0));
    }
    
    [Test]
    public void it_should_throw_exception_if_size_lt_zero()
    {
        var sut = new VesselFactory();

        Assert.Throws<ArgumentOutOfRangeException>(() => sut.Create(-50));
    }
}