using BattleshipApi.BusinessLogic.Models;

namespace BattleshipApi.BusinessLogic.Factories;

public class GameFactory : IGameFactory
{
    private readonly IPlayerFactory playerFactory;

    public GameFactory(IPlayerFactory playerFactory)
    {
        this.playerFactory = playerFactory;
    } 
    
    public BattleshipGame Create(List<string> playerNames)
    {
        var x = new BattleshipGame(playerFactory);
        x.NewGame(playerNames);

        return x;
    }
}