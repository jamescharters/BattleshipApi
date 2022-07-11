using BattleshipApi.BusinessLogic.Models;

namespace BattleshipApi.BusinessLogic.Interfaces;

public interface IPlayerFactory
{
    Player Create(string name);
}