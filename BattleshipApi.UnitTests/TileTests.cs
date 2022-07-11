using BattleshipApi.BusinessLogic.Models;

namespace BattleshipApi.UnitTests;

public class TileTests
{
    [Test]
    public void it_should_create_tile()
    {
        var sut = new Tile(1, 2, TileType.Vessel);

        Assert.AreEqual(1, sut.Coordinates.Row);
        Assert.AreEqual(2, sut.Coordinates.Column);
        Assert.AreEqual(TileType.Vessel, sut.Type);
    }
}