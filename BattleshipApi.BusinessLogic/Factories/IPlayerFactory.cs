using BattleshipApi.BusinessLogic.Models;

namespace BattleshipApi.BusinessLogic.Factories;

public interface IPlayerFactory
{
    Player Create(string name);
}