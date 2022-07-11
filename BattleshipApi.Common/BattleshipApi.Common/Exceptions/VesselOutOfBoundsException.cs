namespace BattleshipApi.Common.Exceptions;

public class VesselOutOfBoundsException : Exception
{
    public VesselOutOfBoundsException(string message) : base(message)
    {
    }
}