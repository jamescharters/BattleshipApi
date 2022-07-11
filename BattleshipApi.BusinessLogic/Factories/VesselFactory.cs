using System.Text;
using BattleshipApi.BusinessLogic.Interfaces;
using BattleshipApi.BusinessLogic.Models;

namespace BattleshipApi.BusinessLogic.Factories;

public class VesselFactory : IVesselFactory
{
    private readonly NamesGenerator namesGenerator;

    public VesselFactory()
    {
        namesGenerator = new NamesGenerator();
    }

    public Vessel Create(int size)
    {
        if (size <= 0)
        {
            throw new ArgumentOutOfRangeException($"{size}");
        }
        
        return new Vessel(namesGenerator.GetRandomName(), size);
    }
}