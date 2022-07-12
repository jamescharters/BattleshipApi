using BattleshipApi.Common.Models;

namespace BattleshipApi.UnitTests.Common;

public class CartesianCoordinatesTests
{
    [Test]
    public void it_should_throw_exception_if_X_lt_zero()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new CartesianCoordinates(-1, 0));
    }

    [Test]
    public void it_should_throw_exception_if_Y_lt_zero()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new CartesianCoordinates(0, -1));
    }
}