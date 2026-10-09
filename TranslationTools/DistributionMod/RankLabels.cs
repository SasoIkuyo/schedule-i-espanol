using System.Text.RegularExpressions;

namespace ScheduleISpanish;

public static class RankLabels
{
    private const string Names="Street Rat|Hoodlum|Peddler|Hustler|Bagman|Enforcer|Shot Caller|Block Boss|Underlord|Baron|Kingpin";
    private const string Roman="X|IX|VIII|VII|VI|V|IV|III|II|I";
    private static readonly Regex Rank=new(@"\b(?:"+Names+@")\s+(?<roman>"+Roman+@")\b",RegexOptions.CultureInvariant|RegexOptions.IgnoreCase,TimeSpan.FromMilliseconds(10));
    private static readonly Regex Label=new(@"^(?<unlock>Unlocks at )?(?<rank>"+Names+@")\s+(?<roman>"+Roman+@")$",RegexOptions.CultureInvariant|RegexOptions.IgnoreCase,TimeSpan.FromMilliseconds(10));
    private static readonly Dictionary<string,string> Spanish=new(StringComparer.OrdinalIgnoreCase) {
        ["Street Rat"]="Rata callejera",["Hoodlum"]="Maleante",["Peddler"]="Vendedor ambulante",
        ["Hustler"]="Estafador",["Bagman"]="Recaudador",["Enforcer"]="Matón",
        ["Shot Caller"]="Cabecilla",["Block Boss"]="Jefe del barrio",["Underlord"]="Capo",
        ["Baron"]="Barón",["Kingpin"]="Capo supremo"
    };

    public static bool TrySpanish(string source,out string translation)
    {
        translation=source;
        var match=Label.Match(source);
        if(!match.Success) return false;
        translation=(match.Groups["unlock"].Success ? "Requiere " : "")+Spanish[match.Groups["rank"].Value]+" "+match.Groups["roman"].Value.ToUpperInvariant();
        return true;
    }

    public static string ProtectNumerals(string source,Func<string,string> protect) => Rank.Replace(source,m=> {
        var numeral=m.Groups["roman"];
        int offset=numeral.Index-m.Index;
        return m.Value[..offset]+protect(numeral.Value);
    });

    public static bool NumeralsPreserved(string source,string translation)
    {
        var expected=Rank.Matches(source).Cast<Match>().Select(m=>m.Groups["roman"].Value).GroupBy(s=>s);
        foreach(var group in expected)
            if(Regex.Matches(translation,@"\b"+Regex.Escape(group.Key)+@"\b",RegexOptions.CultureInvariant,TimeSpan.FromMilliseconds(10)).Count<group.Count()) return false;
        return true;
    }
}
