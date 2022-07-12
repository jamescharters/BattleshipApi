using BattleshipApi.Core.Interfaces;
using BattleshipApi.Core.Models;
using RandomNameGeneratorLibrary;

namespace BattleshipApi.Core.Factories;

public class VesselFactory : IVesselFactory
{
    private readonly PersonNameGenerator namesGenerator;

    public VesselFactory()
    {
        namesGenerator = new PersonNameGenerator();
    }

    public virtual Vessel Create(int size)
    {
        if (size <= 0)
        {
            throw new ArgumentOutOfRangeException($"{size}");
        }
        
        return new Vessel(namesGenerator.GenerateRandomFirstAndLastName(), size);
    }
}