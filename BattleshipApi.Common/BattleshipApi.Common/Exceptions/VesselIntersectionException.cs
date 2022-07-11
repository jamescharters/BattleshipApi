namespace BattleshipApi.Common.Exceptions;

public class VesselIntersectionException : Exception
{
    public VesselIntersectionException(string message) : base(message)
    {
    }
}