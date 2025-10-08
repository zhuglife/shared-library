namespace Common.Extensions;

/// <summary>
/// Extension methods for DateTime operations
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// Determines whether the specified date falls on a weekend (Saturday or Sunday)
    /// </summary>
    /// <param name="date">The date to check</param>
    /// <returns>True if the date is Saturday or Sunday; otherwise, false</returns>
    public static bool IsWeekend(this DateTime date)
    {
        return date.DayOfWeek == DayOfWeek.Saturday || 
               date.DayOfWeek == DayOfWeek.Sunday;
    }

    /// <summary>
    /// Determines whether the specified date falls on a weekday (Monday through Friday)
    /// </summary>
    /// <param name="date">The date to check</param>
    /// <returns>True if the date is Monday through Friday; otherwise, false</returns>
    public static bool IsWeekday(this DateTime date)
    {
        return !date.IsWeekend();
    }

    /// <summary>
    /// Returns the start of the day (midnight) for the specified date
    /// </summary>
    /// <param name="date">The date to process</param>
    /// <returns>A DateTime representing midnight (00:00:00) of the specified date</returns>
    public static DateTime StartOfDay(this DateTime date)
    {
        return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, date.Kind);
    }

    /// <summary>
    /// Returns the end of the day (23:59:59.999) for the specified date
    /// </summary>
    /// <param name="date">The date to process</param>
    /// <returns>A DateTime representing the last moment of the specified date</returns>
    public static DateTime EndOfDay(this DateTime date)
    {
        return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999, date.Kind);
    }

    /// <summary>
    /// Returns the start of the month (first day at midnight) for the specified date
    /// </summary>
    /// <param name="date">The date to process</param>
    /// <returns>A DateTime representing the first day of the month at midnight</returns>
    public static DateTime StartOfMonth(this DateTime date)
    {
        return new DateTime(date.Year, date.Month, 1, 0, 0, 0, date.Kind);
    }

    /// <summary>
    /// Returns the end of the month (last day at 23:59:59.999) for the specified date
    /// </summary>
    /// <param name="date">The date to process</param>
    /// <returns>A DateTime representing the last moment of the month</returns>
    public static DateTime EndOfMonth(this DateTime date)
    {
        var lastDay = DateTime.DaysInMonth(date.Year, date.Month);
        return new DateTime(date.Year, date.Month, lastDay, 23, 59, 59, 999, date.Kind);
    }
}
