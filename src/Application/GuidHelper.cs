using System;

namespace BikeService.Application;

public static class GuidHelper
{
    /// <summary>Non-crashing constructor for Guid</summary>
    /// <param name="input">String representation of Guid</param>
    /// <returns>Guid from input or an empty Guid</returns>
    public static Guid GuidOrEmpty(string input)
    {
        try
        {
            return new Guid(input);
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