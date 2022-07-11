using BattleshipApi.BusinessLogic.Models;

namespace BattleshipApi.Repository;

public class GameRepository : IGameRepository
{
    // DEVNOTE: this is intentionally a *very* naive, unsafe, inmemory repository of Battleship games for the API

    private List<Game> battleshipGames;

    public GameRepository()
    {
        battleshipGames = new List<Game>();
    }

    public void Add(Game game)
    {
        battleshipGames.Add(game);
    }

    public Game? Get(Guid gameId)
    {
        return battleshipGames.SingleOrDefault(_ => _.GameId == gameId);
    }
}