using BattleshipApi.BusinessLogic.Interfaces;
using BattleshipApi.BusinessLogic.Models;

namespace BattleshipApi.BusinessLogic.Factories;

public class GameFactory : IGameFactory
{
    private readonly IPlayerFactory playerFactory;

    public GameFactory(IPlayerFactory playerFactory)
    {
        this.playerFactory = playerFactory;
    }

    public Game Create(List<string> playerNames)
    {
        if (!playerNames.Any())
        {
            throw new ArgumentException($"{nameof(playerNames)} does not contain any elements!");
        }
        
        var newGame = new Game(playerFactory);
        
        newGame.Initialise(playerNames);

        return newGame;
    }
}