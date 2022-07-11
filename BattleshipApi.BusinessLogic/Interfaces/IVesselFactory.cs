using BattleshipApi.BusinessLogic.Models;

namespace BattleshipApi.BusinessLogic.Interfaces;

public interface IVesselFactory
{
    Vessel Create(int size);
}