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
    
    public Vessel Create(int health)
    {
        return new Vessel(namesGenerator.GetRandomName(), health);
    }
}