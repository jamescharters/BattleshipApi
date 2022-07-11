using BattleshipApi.BusinessLogic.Models;

namespace BattleshipApi.BusinessLogic.Factories;

public interface IVesselFactory
{
    Vessel Create(int health);
}