using BattleshipApi.BusinessLogic.Factories;
using BattleshipApi.BusinessLogic.Models;
using BattleshipApi.Common.Enums;
using BattleshipApi.Common.Models;

namespace BattleshipApi.UnitTests;

public class BattleshipGameTests
{
    [Test]
    public void it_should_return_Miss_on_fire_miss()
    {
        var sut = createGame();

        var currentPlayer = sut.Players.Single();

        Assert.AreEqual(FireResult.Miss, sut.FireAt(currentPlayer.Id, new Coordinate(0, 0)));
    }

    [Test]
    public void it_should_return_Hit_on_fire_vessel_hit()
    {
        var sut = createGame();

        var currentPlayer = sut.Players.Single();

        var vessel1 = new Vessel("Vessel 1", 3);
        var vessel2 = new Vessel("Vessel 2", 2);

        currentPlayer.AddVessel(new Coordinate(2, 6), VesselOrientation.Vertical, vessel1);
        currentPlayer.AddVessel(new Coordinate(0, 0), VesselOrientation.Horizontal, vessel2);

        Assert.AreEqual(FireResult.Hit, sut.FireAt(currentPlayer.Id, new Coordinate(0, 6)));
    }

    [Test]
    public void it_should_return_OutOfBounds_if_target_is_out_of_bounds()
    {
        var sut = createGame();

        var currentPlayer = sut.Players.Single();

        Assert.AreEqual(FireResult.OutOfBounds, sut.FireAt(currentPlayer.Id, new Coordinate(500, 500)));
    }

    [Test]
    public void it_should_return_AlreadyFiredAt_if_target_already_fired_at()
    {
        var sut = createGame();

        var currentPlayer = sut.Players.Single();

        // Fire at a position
        sut.FireAt(currentPlayer.Id, new Coordinate(1, 1));

        // Fire at the same position
        Assert.AreEqual(FireResult.AlreadyFiredAt, sut.FireAt(currentPlayer.Id, new Coordinate(1, 1)));
    }

    private Game createGame()
    {
        var sut = new Game(new PlayerFactory());

        sut.Initialise("Salty Dawg");

        return sut;
    }
}