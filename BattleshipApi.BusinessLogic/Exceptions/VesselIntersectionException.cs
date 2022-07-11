namespace BattleshipApi.BusinessLogic.Exceptions;

public class VesselIntersectionException : Exception
{
    public VesselIntersectionException(string message) : base(message)
    {
    }
}