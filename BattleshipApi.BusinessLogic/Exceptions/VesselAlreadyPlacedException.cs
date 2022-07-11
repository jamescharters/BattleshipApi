namespace BattleshipApi.BusinessLogic.Exceptions;

public class VesselAlreadyPlacedException : Exception
{
    public VesselAlreadyPlacedException(string message) : base(message)
    {
    }
}