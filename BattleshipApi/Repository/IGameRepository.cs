using BattleshipApi.BusinessLogic.Models;

namespace BattleshipApi.Repository;

public interface IGameRepository
{
    void Add(Game game);
    Game? Get(Guid gameId);
}