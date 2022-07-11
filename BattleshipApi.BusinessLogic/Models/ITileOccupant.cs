namespace BattleshipApi.BusinessLogic.Models;

public interface ITileOccupant
{
    string Name { get; }
    void AddDamage(int amount = 1);
    bool IsDead { get; }
    TileType Type { get; }
}