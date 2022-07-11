using System.Text;
using BattleshipApi.BusinessLogic.Models;

namespace BattleshipApi.BusinessLogic.Factories;

public class VesselFactory : IVesselFactory
{
    private readonly NamesGenerator namesGenerator;

    public VesselFactory()
    {
        namesGenerator = new NamesGenerator();
    }
    
    public Vessel Create(int vesselSize = 1)
    {
        return new Vessel
        {
            Id = Guid.NewGuid(),
            Damage = 0,
            Size = vesselSize,
            Name = namesGenerator.GetRandomName()
        };
    }
}