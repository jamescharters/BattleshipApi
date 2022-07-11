using BattleshipApi.BusinessLogic.Models;

namespace BattleshipApi.BusinessLogic.Factories;

public interface IGameFactory
{
    BattleshipGame Create(List<string> playerNames);
}