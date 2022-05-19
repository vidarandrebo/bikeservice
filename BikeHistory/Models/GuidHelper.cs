namespace BikeHistory.Models;

public static class GuidHelper
{
    // Returns a Guid if one can be created the incoming value, returns an empty Guid otherwise
    public static Guid GuidOrEmpty(string value)
    {
        try
        {
            return new Guid(value);
        }
        catch (Exception ex)
        {
            switch (ex)
            {
                case FormatException:
                case OverflowException:
                case ArgumentNullException:
                    return Guid.Empty;
                default:
                    throw;
            }
        }
    }
}