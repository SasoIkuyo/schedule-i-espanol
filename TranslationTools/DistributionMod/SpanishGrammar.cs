using System.Text.RegularExpressions;

namespace ScheduleISpanish;

public static class SpanishGrammar
{
    private static readonly Regex Tags=new(@"<[^>]*>",RegexOptions.CultureInvariant,TimeSpan.FromMilliseconds(10));
    private static readonly Regex Duration=new(@"^\(?\d+ (?:days?|hours?|minutes?)(?:,?\s+\d+ (?:days?|hours?|minutes?))* remaining\)?$",RegexOptions.CultureInvariant,TimeSpan.FromMilliseconds(10));
    private static readonly Regex TimeSpanValue=new(@"^\d+ (?:days?|hours?|minutes?)(?:,?\s+\d+ (?:days?|hours?|minutes?))*$",RegexOptions.CultureInvariant,TimeSpan.FromMilliseconds(10));
    private static readonly Regex RegionRequirement=new(@"^'(?<region>[A-Z][A-Z ]{1,40})' REGION MUST BE UNLOCKED$",RegexOptions.CultureInvariant,TimeSpan.FromMilliseconds(10));
    public static bool TryRegionRequirement(string source,out string translation)
    {
        translation=source;
        var match=RegionRequirement.Match(source);
        if(!match.Success) return false;
        string region=match.Groups["region"].Value;
        // Keep the location name recognizable on the map.
        region=System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase(region.ToLowerInvariant());
        translation="Desbloquea "+region;
        return true;
    }
    private static readonly Regex Units=new(@"\b(day|days|hour|hours|minute|minutes|remaining)\b",RegexOptions.CultureInvariant,TimeSpan.FromMilliseconds(10));
    public static bool TryDuration(string source,out string translation)
    {
        translation=source;
        string plain=Tags.Replace(source,"");
        if(!Duration.IsMatch(plain)) return false;
        translation=Units.Replace(source,m=>m.Value switch {"day"=>"día","days"=>"días","hour"=>"hora","hours"=>"horas","minute"=>"minuto","minutes"=>"minutos",_=>"restantes"});
        if(Regex.IsMatch(plain,@"^\(?1 (day|hour|minute) remaining\)?$")) translation=translation.Replace("restantes","restante",StringComparison.Ordinal);
        return true;
    }

    public static string TranslateTimeSpan(string value)
    {
        if(!TimeSpanValue.IsMatch(Tags.Replace(value,""))) return value;
        return Units.Replace(value,m=>m.Value switch {"day"=>"día","days"=>"días","hour"=>"hora","hours"=>"horas","minute"=>"minuto","minutes"=>"minutos",_=>m.Value});
    }
}
