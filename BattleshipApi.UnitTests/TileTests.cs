using BattleshipApi.Core.Models;

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
    
    [Test]
    public void it_should_equal_true_if_two_tiles_are_the_same()
    {
        var tile1 = new Tile(1, 2, TileType.Vessel);
        var tile2 = new Tile(1, 2, TileType.Vessel);
            
        Assert.IsTrue(tile1.Equals(tile2));
    }
    
    [Test]
    public void it_should_equal_false_if_two_tiles_are_not_the_same()
    {
        var tile1 = new Tile(1, 2, TileType.Vessel);
        var tile2 = new Tile(2, 2, TileType.Vessel);
            
        Assert.IsFalse(tile1.Equals(tile2));
    }
}