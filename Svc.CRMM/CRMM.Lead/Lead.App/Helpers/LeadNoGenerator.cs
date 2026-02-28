namespace Lead.App.Helpers;

public static class LeadNoGenerator
{
    public static string NewLeadNo(DateTime now)
    {
        var utcNow = now.Kind == DateTimeKind.Utc ? now : now.ToUniversalTime();
        var ticks = utcNow.Ticks;
        return $"LEAD-{utcNow:yyyyMMdd}-{ticks.ToString().Substring(Math.Max(0, ticks.ToString().Length - 6))}";
    }
}
