using BattleshipApi.BusinessLogic.Models;

namespace BattleshipApi.UnitTests;

public class CoordinateTests
{
    [Test]
    public void it_should_throw_exception_if_X_lt_zero()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Coordinate(-1, 0));   
    }
    
    [Test]
    public void it_should_throw_exception_if_Y_lt_zero()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Coordinate(0, -1));
    }
}