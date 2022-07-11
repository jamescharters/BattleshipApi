using BattleshipApi.BusinessLogic.Models;

namespace BattleshipApi.BusinessLogic.Interfaces;

public interface IGameFactory
{
    Game Create(List<string> playerNames);
}