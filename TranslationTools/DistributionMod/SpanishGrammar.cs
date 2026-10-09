using System.Text.RegularExpressions;

namespace ScheduleISpanish;

public static class SpanishGrammar
{
    private static readonly Regex Tags=new(@"<[^>]*>",RegexOptions.CultureInvariant,TimeSpan.FromMilliseconds(10));
    private static readonly Regex Duration=new(@"^\(?\d+ (?:days?|hours?|minutes?)(?:,?\s+\d+ (?:days?|hours?|minutes?))* remaining\)?$",RegexOptions.CultureInvariant,TimeSpan.FromMilliseconds(10));
    private static readonly Regex TimeSpanValue=new(@"^\d+ (?:days?|hours?|minutes?)(?:,?\s+\d+ (?:days?|hours?|minutes?))*$",RegexOptions.CultureInvariant,TimeSpan.FromMilliseconds(10));
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
