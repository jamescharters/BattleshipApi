using BattleshipApi.Core.Interfaces;
using BattleshipApi.Core.Models;

namespace BattleshipApi.Core.Factories;

public class GameFactory : IGameFactory
{
    private readonly IPlayerFactory playerFactory;

    public GameFactory(IPlayerFactory playerFactory)
    {
        this.playerFactory = playerFactory;
    }

    public virtual Game Create(params string[] playerNames)
    {
        if (playerNames == null || !playerNames.Any())
        {
            throw new ArgumentNullException($"{nameof(playerNames)} does not contain any elements!");
        }

        if (playerNames.Any(string.IsNullOrWhiteSpace))
        {
            throw new ArgumentOutOfRangeException($"Empty player names are not permitted!");
        }

        var newGame = new Game(playerFactory);

        newGame.Initialise(playerNames);

        return newGame;
    }
}