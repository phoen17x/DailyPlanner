namespace DailyPlanner.Common.Extensions;

public static class GuidExtensions
{
    public static string Shrink(this Guid id)
    {
        return id.ToString().Replace("-", "").Replace(" ", "");
    }
}