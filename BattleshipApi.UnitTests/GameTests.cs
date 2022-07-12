using BattleshipApi.Core.Factories;
using BattleshipApi.Core.Models;
using BattleshipApi.Common.Enums;
using BattleshipApi.Common.Exceptions;
using BattleshipApi.Common.Models;
using Moq;

namespace BattleshipApi.UnitTests;

public class GameTests
{
    private Mock<PlayerFactory> mockPlayerFactory;

    public GameTests()
    {
        mockPlayerFactory = new Mock<PlayerFactory>();

        mockPlayerFactory.Setup(_ => _.Create(It.IsAny<string>())).Returns<string>(_ => new Player(_));
    }

    [Test]
    public void it_should_throw_exception_on_initialisation_if_no_player_names()
    {
        var sut = new Game(mockPlayerFactory.Object);
        
        Assert.Throws<ArgumentNullException>(() => sut.Initialise());
    }
    
    [Test]
    public void it_should_return_Miss_on_fire_miss()
    {
        var sut = createAndInitialiseGame();

        var currentPlayer = sut.Players.Single();

        Assert.AreEqual(FireResult.Miss, sut.FireAt(currentPlayer.Id, new CartesianCoordinates(0, 0)));
    }

    [Test]
    public void it_should_return_Hit_on_fire_vessel_hit()
    {
        var sut = createAndInitialiseGame();

        var currentPlayer = sut.Players.Single();

        var vessel1 = new Vessel("Vessel 1", 3);
        var vessel2 = new Vessel("Vessel 2", 2);

        currentPlayer.AddVessel(new CartesianCoordinates(2, 6), VesselOrientation.Vertical, vessel1);
        currentPlayer.AddVessel(new CartesianCoordinates(0, 0), VesselOrientation.Horizontal, vessel2);

        Assert.AreEqual(FireResult.Hit, sut.FireAt(currentPlayer.Id, new CartesianCoordinates(0, 0)));
    }

    [Test]
    public void it_should_return_OutOfBounds_if_target_is_out_of_bounds()
    {
        var sut = createAndInitialiseGame();

        var currentPlayer = sut.Players.Single();

        Assert.AreEqual(FireResult.OutOfBounds, sut.FireAt(currentPlayer.Id, new CartesianCoordinates(500, 500)));
    }

    [Test]
    public void it_should_return_AlreadyFiredAt_if_target_already_fired_at()
    {
        var sut = createAndInitialiseGame();

        var currentPlayer = sut.Players.Single();

        // Fire at a position
        sut.FireAt(currentPlayer.Id, new CartesianCoordinates(1, 1));

        // Fire at the same position
        Assert.AreEqual(FireResult.AlreadyFiredAt, sut.FireAt(currentPlayer.Id, new CartesianCoordinates(1, 1)));
    }

    [Test]
    public void it_should_throw_exception_if_player_does_not_exist()
    {
        var sut = createAndInitialiseGame();

        Assert.Throws<InvalidPlayerException>(() => sut.FireAt(Guid.NewGuid(), new CartesianCoordinates(1, 1)));
    }

    private Game createAndInitialiseGame()
    {
        var sut = new Game(mockPlayerFactory.Object);

        sut.Initialise("Salty Dawg");

        return sut;
    }
}