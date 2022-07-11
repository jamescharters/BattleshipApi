namespace BattleshipApi.Models;

public class Player
{
    public ShipBoard ShipBoard { get; set; }
    public ShotBoard ShotBoard { get; set; }
    public IEnumerable<Vessel> Vessels { get; set; }
    
}