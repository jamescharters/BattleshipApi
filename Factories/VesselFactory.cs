using System.Text;
using BattleshipApi.Models;

namespace BattleshipApi.Factories;

public static class VesselFactory
{
    private static readonly NamesGenerator namesGenerator = new();

    public static Vessel Create()
    {
        return new Vessel
        {
            Name = namesGenerator.GetRandomName(),
            Type = OccupationType.Ship
        };
    }
}