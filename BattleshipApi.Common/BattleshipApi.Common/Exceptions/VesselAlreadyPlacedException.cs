namespace BattleshipApi.Common.Exceptions;

public class VesselAlreadyPlacedException : Exception
{
    public VesselAlreadyPlacedException(string message) : base(message)
    {
    }
}