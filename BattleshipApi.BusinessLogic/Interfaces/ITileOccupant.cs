using BattleshipApi.BusinessLogic.Models;

namespace BattleshipApi.BusinessLogic.Interfaces;

public interface ITileOccupant
{
    Guid Id { get; }
    string Name { get; }
    void AddDamage(int amount = 1);
    bool IsDead { get; }
    TileType Type { get; }
}