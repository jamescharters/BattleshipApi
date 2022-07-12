using BattleshipApi.Core.Interfaces;
using BattleshipApi.Core.Models;

namespace BattleshipApi.Core.Factories;

public class PlayerFactory : IPlayerFactory
{
    public virtual Player Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException($"{nameof(name)}");
        }

        return new Player(name);
    }
}