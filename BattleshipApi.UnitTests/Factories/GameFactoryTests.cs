using BattleshipApi.Core.Factories;
using BattleshipApi.Core.Interfaces;
using BattleshipApi.Core.Models;
using Moq;

namespace BattleshipApi.UnitTests.Factories;

public class GameFactoryTests
{
    private readonly Player mockPlayer = new("Fake Player");
    private readonly Mock<IPlayerFactory> mockPlayerFactory = new();

    public GameFactoryTests()
    {
        mockPlayerFactory.Setup(_ => _.Create(It.IsAny<string>())).Returns(mockPlayer);
    }
    
    [Test]
    public void it_should_create_game_if_name_is_specified()
    {
        var sut = new GameFactory(mockPlayerFactory.Object);

        var result = sut.Create("Somebody");
        
        Assert.AreNotEqual(Guid.Empty, result.GameId);
        Assert.IsNotEmpty(result.Players);
    }
    
    [Test]
    public void it_should_throw_exception_if_no_names_are_specified()
    {
        var sut = new GameFactory(mockPlayerFactory.Object);

        Assert.Throws<ArgumentNullException>(() => sut.Create());
    }
    
    [Test]
    public void it_should_throw_exception_if_empty_player_names_are_specified()
    {
        var sut = new GameFactory(mockPlayerFactory.Object);

        Assert.Throws<ArgumentOutOfRangeException>(() => sut.Create("Joe", ""));
    }
}