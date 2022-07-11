using BattleshipApi.BusinessLogic.Models;

namespace BattleshipApi.BusinessLogic.Factories;

public class PlayerFactory : IPlayerFactory
{
    public Player Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentOutOfRangeException($"{nameof(name)}");
        }

        return new Player(name);
    }
}