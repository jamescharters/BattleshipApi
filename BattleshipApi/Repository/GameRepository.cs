using BattleshipApi.BusinessLogic.Models;

namespace BattleshipApi.Repository;

public interface IGameRepository
{
    void Add(BattleshipGame game);
    BattleshipGame? Get(Guid gameId);
}

public class GameRepository : IGameRepository
{
    // DEVNOTE: this is intentionally a *very* naive, unsafe, inmemory repository of Battleship games for the API
    
    private List<BattleshipGame> battleshipGames;

    public GameRepository()
    {
        battleshipGames = new List<BattleshipGame>();
    }

    public void Add(BattleshipGame game)
    {
        battleshipGames.Add(game);
    }

    public BattleshipGame? Get(Guid gameId)
    {
        return battleshipGames.SingleOrDefault(_ => _.GameId == gameId);
    }
}