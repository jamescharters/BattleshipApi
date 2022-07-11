using BattleshipApi.BusinessLogic.Exceptions;
using BattleshipApi.BusinessLogic.Factories;
using BattleshipApi.BusinessLogic.Models;

namespace BattleshipApi.UnitTests;

public class BattleshipGameTests
{
    [Test]
    public void it_should_return_false_on_fire_miss()
    {
        var sut = new BattleshipGame(new PlayerFactory());

        sut.NewGame("Salty Dawg");
        
        Assert.IsFalse(sut.FireAtTarget(Guid.Empty, new Coordinate(0, 0)));
    }
    
    [Test]
    public void it_should_return_true_on_fire_vessel_hit()
    {
        var sut = new BattleshipGame(new PlayerFactory());
        
        sut.NewGame("Salty Dawg");

        var currentPlayer = sut.Players.Single();
        
        currentPlayer.VesselBoard.AddVessel(new Coordinate(0, 0), VesselOrientation.Horizontal, 5);
        
        Assert.IsTrue(sut.FireAtTarget(currentPlayer.Id, new Coordinate(0, 0)));
    }

    [Test]
    public void it_should_throw_exception_if_target_is_out_of_bounds()
    {
        var sut = new BattleshipGame(new PlayerFactory());
        
        sut.NewGame("Salty Dawg");

        var currentPlayer = sut.Players.Single();
        
        Assert.Throws<TargetOutOfBoundsException>(() => sut.FireAtTarget(currentPlayer.Id, new Coordinate(500, 500)));
    }
}