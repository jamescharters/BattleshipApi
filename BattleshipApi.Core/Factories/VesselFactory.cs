using System.Text;
using BattleshipApi.Core.Interfaces;
using BattleshipApi.Core.Models;

namespace BattleshipApi.Core.Factories;

public class VesselFactory : IVesselFactory
{
    private readonly NamesGenerator namesGenerator;

    public VesselFactory()
    {
        namesGenerator = new NamesGenerator();
    }

    public virtual Vessel Create(int size)
    {
        if (size <= 0)
        {
            throw new ArgumentOutOfRangeException($"{size}");
        }
        
        return new Vessel(namesGenerator.GetRandomName(), size);
    }
}