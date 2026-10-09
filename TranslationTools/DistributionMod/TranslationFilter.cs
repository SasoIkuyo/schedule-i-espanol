using System.Text.RegularExpressions;

namespace ScheduleISpanish;

public static class TranslationFilter
{
    private static readonly Regex Counters=new(@"^(?:[0-9]+(?:[.,][0-9]+)?\s*(?:FPS|ms)|v?[0-9]+(?:\.[0-9]+){1,3}[a-z][0-9]+|\$[0-9.,]+\s*[KMB]?|[0-9]{1,2}:[0-9]{2}\s*(?:AM|PM))$",RegexOptions.IgnoreCase|RegexOptions.CultureInvariant,TimeSpan.FromMilliseconds(10));
    public static bool IsNonLinguistic(string source) => Counters.IsMatch(source.Trim());
}
