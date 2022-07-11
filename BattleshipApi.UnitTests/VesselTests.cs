using BattleshipApi.BusinessLogic.Models;

namespace BattleshipApi.UnitTests;

public class VesselTests
{
    [Test]
    public void it_should_be_dead_if_all_tiles_hit()
    {
        // A vessel is dead if the damage is the same as the number of tiles that it occupies
        
        var sut = new Vessel
        {
            Size = 5
        };
        
        sut.AddDamage(5);

        Assert.IsTrue(sut.IsDead);
    }
    
    [Test]
    public void it_should_not_be_dead_if_damage_lt_size()
    {
        // A damaged vessel =/= dead
        
        var sut = new Vessel
        {
            Size = 5
        };
        
        sut.AddDamage(2);

        Assert.IsFalse(sut.IsDead);
    }
    
    [Test]
    public void it_should_not_allow_damage_to_exceed_size()
    {
        // Crazy high damage should just wrap to the size of the vessel
        
        var sut = new Vessel
        {
            Size = 5
        };
        
        sut.AddDamage(100);

        Assert.AreEqual(5, sut.Size);
    }
    
    [Test]
    public void it_should_not_allow_negative_damage()
    {
        var sut = new Vessel
        {
            Size = 5
        };

        Assert.Throws<ArgumentOutOfRangeException>(() => sut.AddDamage(-7));
    }
}