using BattleshipApi.Core.Models;

namespace BattleshipApi.Core.Interfaces;

public interface IPlayerFactory
{
    Player Create(string name);
}