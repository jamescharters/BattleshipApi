using BattleshipApi.Core.Models;

namespace BattleshipApi.Core.Interfaces;

public interface IVesselFactory
{
    Vessel Create(int size);
}