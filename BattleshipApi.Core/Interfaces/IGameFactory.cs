using BattleshipApi.Core.Models;

namespace BattleshipApi.Core.Interfaces;

public interface IGameFactory
{
    Game Create(params string[] playerNames);
}